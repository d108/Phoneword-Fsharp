namespace PhonewordFsharp

open System
open Foundation
open UIKit
open PhoneTranslator

[<Register ("ViewController")>]
type ViewController (handle:IntPtr) as vc =
    inherit UIViewController (handle)

    let mutable _TranslateButton = null :> UIButton
    let mutable _CallButton = null :> UIButton
    let mutable _PhoneNumberText = null :> UITextField
    let mutable _PhonewordLabel = null :> UILabel

    let translateHandler =
        new EventHandler (fun sender eventargs ->
            Console.WriteLine("Translate")
            _CallButton.SetTitle("Call " + 
                                 PhoneTranslator.ToNumber _PhoneNumberText.Text,
                                 UIControlState.Normal)
    )

    let callHandler =
        new EventHandler (fun sender eventargs ->
            Console.WriteLine("Call")
            // Use URL handler with tel: prefix to invoke Apple's Phone app...            
            let url = new NSUrl("tel:" + PhoneTranslator.ToNumber _PhoneNumberText.Text)
            if (UIApplication.SharedApplication.OpenUrl(url)) then ()

            // ...otherwise show an alert dialog
            let alert 
                = UIAlertController.Create("Not supported",
                                           "Scheme 'tel:' is not supported on this device",
                                           UIAlertControllerStyle.Alert)
            alert.AddAction(
                   UIAlertAction.Create("Ok",
                                        UIAlertActionStyle.Default,
                                        null)
            )
            vc.PresentViewController(alert,
                                     true,
                                     null)            
    )

    // Outlets add KVC compliance for storyboard components

    [<Outlet>]
    member this.TranslateButton
        with get() = _TranslateButton
        and set value = _TranslateButton <- value
    [<Outlet>]
    member this.CallButton
        with get() = _CallButton
        and set value = _CallButton <- value
    [<Outlet>]
    member this.PhoneNumberText
        with get() = _PhoneNumberText
        and set value = _PhoneNumberText <- value
    [<Outlet>]
    member this.PhonewordLabel
        with get() = _PhonewordLabel
        and set value = _PhonewordLabel <- value

    override vc.DidReceiveMemoryWarning () =
        base.DidReceiveMemoryWarning ()

    override vc.ViewDidLoad () =
        base.ViewDidLoad ()
        _TranslateButton.TouchUpInside.AddHandler(translateHandler)
        _CallButton.TouchUpInside.AddHandler(callHandler)

    override vc.ShouldAutorotateToInterfaceOrientation (toInterfaceOrientation) =
        // Return true for supported orientations
        if UIDevice.CurrentDevice.UserInterfaceIdiom = UIUserInterfaceIdiom.Phone then
           toInterfaceOrientation <> UIInterfaceOrientation.PortraitUpsideDown
        else
           true
