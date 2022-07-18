using EAP_C2007L_NguyenDucHuy.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace EAP_C2007L_NguyenDucHuy.Controllers
{
    public class JsonWSController : ApiController
    {
        private MyDbContext db = new MyDbContext();
        // GET: JsonWS
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Get()
        {
            var albums = db.Albums.ToList();
            JToken json = MyHelper.ToJson(albums);
            return Request.CreateResponse<JToken>(json);
           
        }
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var album = db.Albums.Where(x=>x.AlbumId==id);
            JToken json = MyHelper.ToJson(album);
            return Request.CreateResponse<JToken>(json);

        }
    }
    public static class MyHelper
    {
        public static JToken ToJson<T>(T source)
        {
            JObject result = new JObject();

            Type type = typeof(T);
            string key = type.Name;
            if (typeof(IEnumerable).IsAssignableFrom(type))
                key = string.Format("ArrayOf{0}", GetEnumeratedType(type).Name);

            JToken value = (source != null) ? JToken.FromObject(source) : JValue.CreateNull();//Newtonsoft.Json 6.0.6
            result.Add(key, value);
            return result;
        }

        /// <summary>
        /// Use http://stackoverflow.com/questions/4129831/how-do-i-get-the-array-item-type-from-array-type-in-net
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetEnumeratedType(Type type)
        {
            // provided by Array
            var elType = type.GetElementType();
            if (null != elType) return elType;

            // otherwise provided by collection
            var elTypes = type.GetGenericArguments();
            if (elTypes.Length > 0) return elTypes[0];

            // otherwise is not an 'enumerated' type
            return null;
        }
    }
}