namespace Sabermetrics
module HtmlHandler =
    open System.Text.RegularExpressions
    let getHtmlSection tag html =
        let regex = sprintf "<{tag}[ a-zA-Z0-9=\"-]*>(.|\n|\r)*<\\{tag}>"
        Regex(regex).Match(html).Value
    let getTable html =
        getHtmlSection "table" html
    let getHeaders html =
        getHtmlSection "th" html

