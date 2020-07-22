namespace Sabermetrics

module DataCollection =
    open System
    open System.Net
    open System.Text.RegularExpressions
    open System.IO

    type ResponseWrapper =
    | Html of WebResponse
    | Other
    let getSite url =
        let request = WebRequest.Create (url:Uri) : WebRequest
        request.GetResponse()
        
    let getBaseballSite =
        getSite (new Uri(@"https://www.baseball-reference.com/"))

    let wrapResonse (response: WebResponse) =
        let isHtml = Regex("html").IsMatch(response.ContentType)
        match isHtml with
        | true -> Html response
        | false -> Other

    let generateLetters first last =
        [first..last] |> List.map string


    let handleResponse (response:ResponseWrapper) =
        match response with
        | Html r ->
            use stream = r.GetResponseStream()
            use reader = new StreamReader(stream)
            let html = reader.ReadToEnd()
            Some html
        | _ -> None

