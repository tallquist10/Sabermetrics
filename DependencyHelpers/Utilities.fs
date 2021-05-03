namespace Sabermetrics
module Result =
    let isOk result = 
        match result with
        | Result.Ok _ -> true
        | Result.Error _ -> false

    let isError result = not (isOk result)

module List =

    /// <summary>
    /// Get the values of all ok results in a list
    /// </summary>
    /// <param name="list">A list of results</param>
    let oks (list: Result<'a,'b> list) =
        list
        |> List.filter Result.isOk 

    let errors (list: Result<'a,'b> list) =
        List.filter Result.isError list
