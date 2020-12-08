using IT_AcademyHomework.Common.Validation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace Lesson4_Homework.Controllers
{
    public class TemperatureConverterController : Controller
    {
        private const string _responseTxtFileName = "FahrenheitValue.txt";
        private const string _responseZipFileName = "FahrenheitValueArchive.zip";

        private MemoryStream _responseByteStream;

        private readonly IValidator<double> _validator;

        public TemperatureConverterController(IValidator<double> validator) 
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public IActionResult CelsiumToFahrenheits(double celsiumTemperature, string responseType = null)
        {
            IActionResult result;

            var validationResult = _validator.Validate(celsiumTemperature);

            if (validationResult.IsValid)
            {
                var fahrenheits = (celsiumTemperature * (9 / 5)) + 32;
                var responseString = $"Result is {fahrenheits} °F";
                result = string.IsNullOrEmpty(responseType) ? Content(responseString) : GenerateResponseWithContent(responseString, responseType);               
            }
            else
            {
                result = ProcessBadValidationResult(validationResult);
            }

            return result;
        }

        private IActionResult GenerateResponseWithContent(string textToPutInFile, string responseType)
        {
            IActionResult result = null;

            if (responseType.Contains("txt"))
            {
                result = new FileContentResult(Encoding.UTF8.GetBytes(textToPutInFile), "text/plain")
                {
                    FileDownloadName = _responseTxtFileName
                };
            }
            else if (responseType.Contains("zip"))
            {
                (string FileName, byte[] Content) fileTuple = (_responseTxtFileName, Encoding.UTF8.GetBytes(textToPutInFile));

                result = new FileContentResult(CreateZipArchive(fileTuple), "application/zip")
                {
                    FileDownloadName = _responseZipFileName
                };
            }
            else if (responseType.Contains("bytes") || responseType.Contains("stream")) 
            {
                _responseByteStream = new MemoryStream(Encoding.UTF8.GetBytes(textToPutInFile));
                result = new FileStreamResult(_responseByteStream, "application/octet-stream");
            }
            return result;
        }

        private byte[] CreateZipArchive((string FileName, byte[] Content) fileTuple)
        {
            byte[] archiveFile;
            using (var archiveStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true))
                {
                    var zipArchiveEntry = archive.CreateEntry(fileTuple.FileName, CompressionLevel.Fastest);
                    using (var zipStream = zipArchiveEntry.Open())
                        zipStream.Write(fileTuple.Content, 0, fileTuple.Content.Length);
                }

                archiveFile = archiveStream.ToArray();
            }

            return archiveFile;
        }

        private BadRequestObjectResult ProcessBadValidationResult(ValidationResult validationResult)
        {
            string errorMessage = string.Empty;

            if (validationResult.ErrorMessages.Any())
            {
                var errors = new StringBuilder();
                validationResult.ErrorMessages.ToList().ForEach(e => errors.AppendLine(e));
                errorMessage = errors.ToString();
            }

            return new BadRequestObjectResult(errorMessage);
        }
    }
}
