using System.Collections.Generic;

namespace Odevez.Utils.Validations
{
    public class Response
    {
        public Response()
        {
            Responses = new List<ResponseBase>();
        }

        public Response(List<ResponseBase> responses)
        {
            Responses = responses;
        }

        public Response(ResponseBase response) : this(new List<ResponseBase>() { response })
        {

        }

        public List<ResponseBase> Responses { get; }
        public static Response<T> OK<T>(T data) => new Response<T>(data);
        public static Response OK() => new Response();
        public static Response BadRequest(List<ResponseBase> responses) => new Response(responses);
        public static Response BadRequest(ResponseBase response) => new Response(response);
    }

    public class Response<T> : Response
    {
        public Response()
        {

        }

        public Response(T data, List<ResponseBase> responses = null) : base(responses)
        {
            Data = data;
        }
        public T Data { get; set; }
    }
}
