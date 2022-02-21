namespace GetirTestApi.CrossCutting
{
    public class ApiHttpError : IApiHttpError
    {
        public string[] ValidationErrors { get; set; }

        public static IApiHttpError CreateHttpValidationError(string[] validationErrors) => new ApiHttpError
        {
            ValidationErrors = validationErrors
        };
    }
}
