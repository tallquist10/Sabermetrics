﻿namespace Sabermetrics.Dependencies
open Sabermetrics.Exceptions
module WebWorker =
    open System
    open System.Text.RegularExpressions
    open System.IO
    open FSharp.Data


    let getSite (url:string) =
        try
            let doc = HtmlDocument.Load(url)
            Result.Ok doc
        with
        | _ -> Result.Error (FailedWebRequestException (sprintf "Failed to retrieve web page for %s" url))

module HtmlHandler =
    open System.Text.RegularExpressions
    open FSharp.Data
    let getHtmlSections tag (doc: Result<HtmlDocument, Exception>) =
        match doc with
        | Result.Ok d -> 
            try
                let sections = CssSelectorExtensions.CssSelect (d,tag)
                match sections with
                |_::_ -> Result.Ok sections
                | [] -> Result.Error (FailedWebResponseParseException (sprintf "Could not find any sections for the selector '%s'" tag))
            with 
            | _ -> Result.Error (FailedWebResponseParseException (sprintf "Could not retrieve sections based on selector '%s'" tag))
        | Result.Error e -> Result.Error e

    let getHtmlSectionByID id doc =
        getHtmlSections (sprintf "#%s" id) doc

    let getInnerContent tag html =
        let regex = sprintf "<%s[ a-zA-Z0-9=\"-]*>(.|\n|\r)*<\\%s>" tag tag
        Regex(regex).Match(html).Groups.Item(0).Value

    let getLinkText html = 
        let regex = "<a[ \w=\"-\/]*>(.|\n|\r)*<\/a>"
        Regex(regex).Match(html).Value

    let getTable html =
        getHtmlSections "table" html

    let getHeaders html =
        getHtmlSections "th" html

    let getRows html =
        getHtmlSections "tr" html

