namespace PhonewordFsharp

open System
open System.Text

module PhoneTranslator =
    let ToNumber(raw: String) =
        if String.IsNullOrWhiteSpace raw then ""
        else
            let upperRaw = raw.ToUpperInvariant()
            let mutable newNumber = StringBuilder()
            let nonLetters =
                [' '; '-'; '0'; '1'; '2'; '3'; '4'; '5'; '6'; '7'; '8'; '9']
            let numbersForLetters =
                ["2", "ABC"
                 "3", "DEF"
                 "4", "GHI"
                 "5", "JKL"
                 "6", "MNO"
                 "7", "PQRS"
                 "8", "TUV"
                 "9", "WXYZ"]
            upperRaw |> Seq.iter (fun chr ->
                let isNonletter chr list =
                    List.exists (fun elem -> elem = chr) list
                let isLetter = (fun chr ->
                    numbersForLetters |> Map.ofList |> Map.exists (fun key value ->
                        value.Contains(chr.ToString())
                    )
                )
                if isNonletter chr nonLetters then
                    newNumber.Append(chr) |> ignore
                else if isLetter chr then
                    for KeyValue(key, value) in dict(numbersForLetters) do
                        if value.Contains(chr.ToString()) then newNumber.Append(key) |> ignore
                else
                    newNumber.Append(chr) |> ignore
            )
            newNumber.ToString()
