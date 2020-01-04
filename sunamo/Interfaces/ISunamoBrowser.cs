﻿


using HtmlAgilityPack;
using System;
using System.Threading.Tasks;

/// <summary>
/// A1 je třída Control závislá na typu cílové aplikace
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ISunamoBrowser<T>
{
    Uri Source { get; set; }
    Task<HtmlDocument> GetHtmlDocument();
    
    string HTML { get; }
    void Navigate(string uri);
    bool ScrollToEnd();
    void Init();
}

