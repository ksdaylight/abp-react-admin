namespace Miwen.Abp.AspNetCore.Wrapper;
public interface IHttpResponseWrapper
{
    void Wrap(HttpResponseWrapperContext context);
}
