namespace PhonewordFsharp

open System
open Foundation
open UIKit

[<Register ("AppDelegate")>]
type AppDelegate () =
    inherit UIApplicationDelegate ()
    override val Window = null with get, set 
    override this.FinishedLaunching (app, options) =
        true
