namespace Portfolio.WebApi.Helpers
{
    public class HttpContextHelper
    {
        public static IHttpContextAccessor Accessor;
        public static HttpRequest Request => Accessor.HttpContext.Request;
        public static HttpResponse Response => Accessor.HttpContext.Response;
        public static IHeaderDictionary ResponseHeaders => Response.Headers;
        public static IHeaderDictionary RequestHeaders => Response.Headers;

        //public static HttpContext Context => Accessor.HttpContext ?? new DefaultHttpContext();
        //public static IHeaderDictionary ResponseHeaders => Context.Response?.Headers ?? new HeaderDictionary();
        public static int UserId => int.Parse(Accessor.HttpContext.User.FindFirst("Id")?.Value ?? "0");
    }
}
