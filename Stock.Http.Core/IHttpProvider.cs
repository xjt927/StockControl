namespace Stock.Http.Core
{
    public interface IHttpProvider
    {
        HttpResponseParameter Excute(HttpRequestParameter requestParameter);
    }
}
