using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Text;

public class SunamoRestJsonTextSerializer : ISerializer
{
    public string ContentType { get; set; }

    public string Serialize(object obj)
    {
        return  JsonSystemTextJson.instance.Serialize(obj);
    }
}