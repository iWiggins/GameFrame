#nullable disable
/// Events for each keyboard key.
/// This code is generated and should not be manually edited.
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameFrame.Core.Input;

public partial class Keyboard : IComponent, IUpdate
{
        
    public Key KeyBack { get; private set; }
        
    public Key KeyTab { get; private set; }
        
    public Key KeyEnter { get; private set; }
        
    public Key KeyCapsLock { get; private set; }
        
    public Key KeyEscape { get; private set; }
        
    public Key KeySpace { get; private set; }
        
    public Key KeyPageUp { get; private set; }
        
    public Key KeyPageDown { get; private set; }
        
    public Key KeyEnd { get; private set; }
        
    public Key KeyHome { get; private set; }
        
    public Key KeyLeft { get; private set; }
        
    public Key KeyUp { get; private set; }
        
    public Key KeyRight { get; private set; }
        
    public Key KeyDown { get; private set; }
        
    public Key KeyPrintScreen { get; private set; }
        
    public Key KeyInsert { get; private set; }
        
    public Key KeyDelete { get; private set; }
        
    public Key KeyA { get; private set; }
        
    public Key KeyB { get; private set; }
        
    public Key KeyC { get; private set; }
        
    public Key KeyD { get; private set; }
        
    public Key KeyE { get; private set; }
        
    public Key KeyF { get; private set; }
        
    public Key KeyG { get; private set; }
        
    public Key KeyH { get; private set; }
        
    public Key KeyI { get; private set; }
        
    public Key KeyJ { get; private set; }
        
    public Key KeyK { get; private set; }
        
    public Key KeyL { get; private set; }
        
    public Key KeyM { get; private set; }
        
    public Key KeyN { get; private set; }
        
    public Key KeyO { get; private set; }
        
    public Key KeyP { get; private set; }
        
    public Key KeyQ { get; private set; }
        
    public Key KeyR { get; private set; }
        
    public Key KeyS { get; private set; }
        
    public Key KeyT { get; private set; }
        
    public Key KeyU { get; private set; }
        
    public Key KeyV { get; private set; }
        
    public Key KeyW { get; private set; }
        
    public Key KeyX { get; private set; }
        
    public Key KeyY { get; private set; }
        
    public Key KeyZ { get; private set; }
        
    public Key KeyLeftWindows { get; private set; }
        
    public Key KeyRightWindows { get; private set; }
        
    public Key KeyNumPad0 { get; private set; }
        
    public Key KeyNumPad1 { get; private set; }
        
    public Key KeyNumPad2 { get; private set; }
        
    public Key KeyNumPad3 { get; private set; }
        
    public Key KeyNumPad4 { get; private set; }
        
    public Key KeyNumPad5 { get; private set; }
        
    public Key KeyNumPad6 { get; private set; }
        
    public Key KeyNumPad7 { get; private set; }
        
    public Key KeyNumPad8 { get; private set; }
        
    public Key KeyNumPad9 { get; private set; }
        
    public Key KeyMultiply { get; private set; }
        
    public Key KeyAdd { get; private set; }
        
    public Key KeySubtract { get; private set; }
        
    public Key KeyDecimal { get; private set; }
        
    public Key KeyDivide { get; private set; }
        
    public Key KeyF1 { get; private set; }
        
    public Key KeyF2 { get; private set; }
        
    public Key KeyF3 { get; private set; }
        
    public Key KeyF4 { get; private set; }
        
    public Key KeyF5 { get; private set; }
        
    public Key KeyF6 { get; private set; }
        
    public Key KeyF7 { get; private set; }
        
    public Key KeyF8 { get; private set; }
        
    public Key KeyF9 { get; private set; }
        
    public Key KeyF10 { get; private set; }
        
    public Key KeyF11 { get; private set; }
        
    public Key KeyF12 { get; private set; }
        
    public Key KeyF13 { get; private set; }
        
    public Key KeyF14 { get; private set; }
        
    public Key KeyF15 { get; private set; }
        
    public Key KeyF16 { get; private set; }
        
    public Key KeyNumLock { get; private set; }
        
    public Key KeyScroll { get; private set; }
        
    public Key KeyLeftShift { get; private set; }
        
    public Key KeyRightShift { get; private set; }
        
    public Key KeyLeftControl { get; private set; }
        
    public Key KeyRightControl { get; private set; }
        
    public Key KeyLeftAlt { get; private set; }
        
    public Key KeyRightAlt { get; private set; }
    
    private void InitializeKeys() {
    
        KeyBack = new(this, Keys.Back);
    
        KeyTab = new(this, Keys.Tab);
    
        KeyEnter = new(this, Keys.Enter);
    
        KeyCapsLock = new(this, Keys.CapsLock);
    
        KeyEscape = new(this, Keys.Escape);
    
        KeySpace = new(this, Keys.Space);
    
        KeyPageUp = new(this, Keys.PageUp);
    
        KeyPageDown = new(this, Keys.PageDown);
    
        KeyEnd = new(this, Keys.End);
    
        KeyHome = new(this, Keys.Home);
    
        KeyLeft = new(this, Keys.Left);
    
        KeyUp = new(this, Keys.Up);
    
        KeyRight = new(this, Keys.Right);
    
        KeyDown = new(this, Keys.Down);
    
        KeyPrintScreen = new(this, Keys.PrintScreen);
    
        KeyInsert = new(this, Keys.Insert);
    
        KeyDelete = new(this, Keys.Delete);
    
        KeyA = new(this, Keys.A);
    
        KeyB = new(this, Keys.B);
    
        KeyC = new(this, Keys.C);
    
        KeyD = new(this, Keys.D);
    
        KeyE = new(this, Keys.E);
    
        KeyF = new(this, Keys.F);
    
        KeyG = new(this, Keys.G);
    
        KeyH = new(this, Keys.H);
    
        KeyI = new(this, Keys.I);
    
        KeyJ = new(this, Keys.J);
    
        KeyK = new(this, Keys.K);
    
        KeyL = new(this, Keys.L);
    
        KeyM = new(this, Keys.M);
    
        KeyN = new(this, Keys.N);
    
        KeyO = new(this, Keys.O);
    
        KeyP = new(this, Keys.P);
    
        KeyQ = new(this, Keys.Q);
    
        KeyR = new(this, Keys.R);
    
        KeyS = new(this, Keys.S);
    
        KeyT = new(this, Keys.T);
    
        KeyU = new(this, Keys.U);
    
        KeyV = new(this, Keys.V);
    
        KeyW = new(this, Keys.W);
    
        KeyX = new(this, Keys.X);
    
        KeyY = new(this, Keys.Y);
    
        KeyZ = new(this, Keys.Z);
    
        KeyLeftWindows = new(this, Keys.LeftWindows);
    
        KeyRightWindows = new(this, Keys.RightWindows);
    
        KeyNumPad0 = new(this, Keys.NumPad0);
    
        KeyNumPad1 = new(this, Keys.NumPad1);
    
        KeyNumPad2 = new(this, Keys.NumPad2);
    
        KeyNumPad3 = new(this, Keys.NumPad3);
    
        KeyNumPad4 = new(this, Keys.NumPad4);
    
        KeyNumPad5 = new(this, Keys.NumPad5);
    
        KeyNumPad6 = new(this, Keys.NumPad6);
    
        KeyNumPad7 = new(this, Keys.NumPad7);
    
        KeyNumPad8 = new(this, Keys.NumPad8);
    
        KeyNumPad9 = new(this, Keys.NumPad9);
    
        KeyMultiply = new(this, Keys.Multiply);
    
        KeyAdd = new(this, Keys.Add);
    
        KeySubtract = new(this, Keys.Subtract);
    
        KeyDecimal = new(this, Keys.Decimal);
    
        KeyDivide = new(this, Keys.Divide);
    
        KeyF1 = new(this, Keys.F1);
    
        KeyF2 = new(this, Keys.F2);
    
        KeyF3 = new(this, Keys.F3);
    
        KeyF4 = new(this, Keys.F4);
    
        KeyF5 = new(this, Keys.F5);
    
        KeyF6 = new(this, Keys.F6);
    
        KeyF7 = new(this, Keys.F7);
    
        KeyF8 = new(this, Keys.F8);
    
        KeyF9 = new(this, Keys.F9);
    
        KeyF10 = new(this, Keys.F10);
    
        KeyF11 = new(this, Keys.F11);
    
        KeyF12 = new(this, Keys.F12);
    
        KeyF13 = new(this, Keys.F13);
    
        KeyF14 = new(this, Keys.F14);
    
        KeyF15 = new(this, Keys.F15);
    
        KeyF16 = new(this, Keys.F16);
    
        KeyNumLock = new(this, Keys.NumLock);
    
        KeyScroll = new(this, Keys.Scroll);
    
        KeyLeftShift = new(this, Keys.LeftShift);
    
        KeyRightShift = new(this, Keys.RightShift);
    
        KeyLeftControl = new(this, Keys.LeftControl);
    
        KeyRightControl = new(this, Keys.RightControl);
    
        KeyLeftAlt = new(this, Keys.LeftAlt);
    
        KeyRightAlt = new(this, Keys.RightAlt);
    
    }

    public IEnumerable<IComponent> Children => [
    this.KeyBack
    
    ,this.KeyTab
    
    ,this.KeyEnter
    
    ,this.KeyCapsLock
    
    ,this.KeyEscape
    
    ,this.KeySpace
    
    ,this.KeyPageUp
    
    ,this.KeyPageDown
    
    ,this.KeyEnd
    
    ,this.KeyHome
    
    ,this.KeyLeft
    
    ,this.KeyUp
    
    ,this.KeyRight
    
    ,this.KeyDown
    
    ,this.KeyPrintScreen
    
    ,this.KeyInsert
    
    ,this.KeyDelete
    
    ,this.KeyA
    
    ,this.KeyB
    
    ,this.KeyC
    
    ,this.KeyD
    
    ,this.KeyE
    
    ,this.KeyF
    
    ,this.KeyG
    
    ,this.KeyH
    
    ,this.KeyI
    
    ,this.KeyJ
    
    ,this.KeyK
    
    ,this.KeyL
    
    ,this.KeyM
    
    ,this.KeyN
    
    ,this.KeyO
    
    ,this.KeyP
    
    ,this.KeyQ
    
    ,this.KeyR
    
    ,this.KeyS
    
    ,this.KeyT
    
    ,this.KeyU
    
    ,this.KeyV
    
    ,this.KeyW
    
    ,this.KeyX
    
    ,this.KeyY
    
    ,this.KeyZ
    
    ,this.KeyLeftWindows
    
    ,this.KeyRightWindows
    
    ,this.KeyNumPad0
    
    ,this.KeyNumPad1
    
    ,this.KeyNumPad2
    
    ,this.KeyNumPad3
    
    ,this.KeyNumPad4
    
    ,this.KeyNumPad5
    
    ,this.KeyNumPad6
    
    ,this.KeyNumPad7
    
    ,this.KeyNumPad8
    
    ,this.KeyNumPad9
    
    ,this.KeyMultiply
    
    ,this.KeyAdd
    
    ,this.KeySubtract
    
    ,this.KeyDecimal
    
    ,this.KeyDivide
    
    ,this.KeyF1
    
    ,this.KeyF2
    
    ,this.KeyF3
    
    ,this.KeyF4
    
    ,this.KeyF5
    
    ,this.KeyF6
    
    ,this.KeyF7
    
    ,this.KeyF8
    
    ,this.KeyF9
    
    ,this.KeyF10
    
    ,this.KeyF11
    
    ,this.KeyF12
    
    ,this.KeyF13
    
    ,this.KeyF14
    
    ,this.KeyF15
    
    ,this.KeyF16
    
    ,this.KeyNumLock
    
    ,this.KeyScroll
    
    ,this.KeyLeftShift
    
    ,this.KeyRightShift
    
    ,this.KeyLeftControl
    
    ,this.KeyRightControl
    
    ,this.KeyLeftAlt
    
    ,this.KeyRightAlt
    
    ];
}