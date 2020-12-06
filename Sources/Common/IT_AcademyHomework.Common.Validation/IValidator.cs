namespace IT_AcademyHomework.Common.Validation
{
    public interface IValidator<T>
    {
        ValidationResult Validate(T parameter);
    }
}
