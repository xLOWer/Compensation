using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using comp_app.MVVM.Model.Common;
using Newtonsoft.Json;

namespace comp_app.Services
{
    public static class JsonService
    {
        public static TEntity Read<TEntity>(string path) => JsonConvert.DeserializeObject<TEntity>(Utilites.DecompressAndUnxor(File.ReadAllText(path)));
        public static void Write<TEntity>(TEntity obj, string path) => File.WriteAllText(  path, Utilites.XorAndCompress(JsonConvert.SerializeObject(obj))  );

    }
}
