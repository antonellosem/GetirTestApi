namespace GetirTestApi.CrossCutting
{
    public interface IApiHttpError
    {
        string[] ValidationErrors { get; set; }
    }
}
