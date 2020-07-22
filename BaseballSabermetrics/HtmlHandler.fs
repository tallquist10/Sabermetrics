namespace Sabermetrics
module HtmlHandler =
    open System.Text.RegularExpressions
    let getHtmlSection tag html =
        let regex = sprintf "<%s[ \w=\"-\/]*>(.|\n|\r)*<\/%s>" tag tag
        Regex(regex).Match(html).Value
    
    // TODO: Refactor to eliminate code duplication
    let getHtmlSections tag html = 
        let regex = sprintf "<%s[ \w=\"-\/]*>(.|\n|\r)*<\/%s>" tag tag
        let sections = Regex(regex).Matches(html)
        [for i in 1..sections.Count -> sections.Item i] |> List.map (fun m -> m.Value)

    let getHtmlSectionByID tag id html =
        let regex = sprintf "<%s[ \w=\"-\/]*id\s?=\"%s\"[ \w=\"-\/]*>(.|\n|\r)*<\/%s>" tag id tag
        let v = Regex(regex).Match(html).Value
        v

    let getInnerContent tag html =
        let regex = sprintf "<%s[ a-zA-Z0-9=\"-]*>(.|\n|\r)*<\\%s>" tag tag
        Regex(regex).Match(html).Groups.Item(0).Value

    let getLinkText html = 
        let regex = "<a[ \w=\"-\/]*>(.|\n|\r)*<\/a>"
        Regex(regex).Match(html).Value

    let getTable html =
        getHtmlSection "table" html

    let getHeaders html =
        getHtmlSection "th" html

    let getRows html =
        getHtmlSections "tr" html


