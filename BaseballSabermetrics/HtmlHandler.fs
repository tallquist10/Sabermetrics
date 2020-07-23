namespace Sabermetrics
module HtmlHandler =
    open System.Text.RegularExpressions
    open FSharp.Data
    let getHtmlSections tag (doc:HtmlDocument) =
        doc.CssSelect(tag)

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


