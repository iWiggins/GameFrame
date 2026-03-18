/// Events for each keyboard key.
/// This code is generated and should not be manually edited.
#nullable disable
using GameFrame.Core;
using GameFrame.Core.Components;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameFrame.Core.Input;

public partial class Keyboard : IComponent, IUpdate
{
        
    Key Back;
        
    Key Tab;
        
    Key Enter;
        
    Key CapsLock;
        
    Key Escape;
        
    Key Space;
        
    Key PageUp;
        
    Key PageDown;
        
    Key End;
        
    Key Home;
        
    Key Left;
        
    Key Up;
        
    Key Right;
        
    Key Down;
        
    Key PrintScreen;
        
    Key Insert;
        
    Key Delete;
        
    Key A;
        
    Key B;
        
    Key C;
        
    Key D;
        
    Key E;
        
    Key F;
        
    Key G;
        
    Key H;
        
    Key I;
        
    Key J;
        
    Key K;
        
    Key L;
        
    Key M;
        
    Key N;
        
    Key O;
        
    Key P;
        
    Key Q;
        
    Key R;
        
    Key S;
        
    Key T;
        
    Key U;
        
    Key V;
        
    Key W;
        
    Key X;
        
    Key Y;
        
    Key Z;
        
    Key LeftWindows;
        
    Key RightWindows;
        
    Key NumPad0;
        
    Key NumPad1;
        
    Key NumPad2;
        
    Key NumPad3;
        
    Key NumPad4;
        
    Key NumPad5;
        
    Key NumPad6;
        
    Key NumPad7;
        
    Key NumPad8;
        
    Key NumPad9;
        
    Key Multiply;
        
    Key Add;
        
    Key Subtract;
        
    Key Decimal;
        
    Key Divide;
        
    Key F1;
        
    Key F2;
        
    Key F3;
        
    Key F4;
        
    Key F5;
        
    Key F6;
        
    Key F7;
        
    Key F8;
        
    Key F9;
        
    Key F10;
        
    Key F11;
        
    Key F12;
        
    Key F13;
        
    Key F14;
        
    Key F15;
        
    Key F16;
        
    Key NumLock;
        
    Key Scroll;
        
    Key LeftShift;
        
    Key RightShift;
        
    Key LeftControl;
        
    Key RightControl;
        
    Key LeftAlt;
        
    Key RightAlt;
    
    private void InitializeKeys() {
    

    
    Back = new(this, Keys.Back);
    
    Tab = new(this, Keys.Tab);
    
    Enter = new(this, Keys.Enter);
    
    CapsLock = new(this, Keys.CapsLock);
    
    Escape = new(this, Keys.Escape);
    
    Space = new(this, Keys.Space);
    
    PageUp = new(this, Keys.PageUp);
    
    PageDown = new(this, Keys.PageDown);
    
    End = new(this, Keys.End);
    
    Home = new(this, Keys.Home);
    
    Left = new(this, Keys.Left);
    
    Up = new(this, Keys.Up);
    
    Right = new(this, Keys.Right);
    
    Down = new(this, Keys.Down);
    
    PrintScreen = new(this, Keys.PrintScreen);
    
    Insert = new(this, Keys.Insert);
    
    Delete = new(this, Keys.Delete);
    
    A = new(this, Keys.A);
    
    B = new(this, Keys.B);
    
    C = new(this, Keys.C);
    
    D = new(this, Keys.D);
    
    E = new(this, Keys.E);
    
    F = new(this, Keys.F);
    
    G = new(this, Keys.G);
    
    H = new(this, Keys.H);
    
    I = new(this, Keys.I);
    
    J = new(this, Keys.J);
    
    K = new(this, Keys.K);
    
    L = new(this, Keys.L);
    
    M = new(this, Keys.M);
    
    N = new(this, Keys.N);
    
    O = new(this, Keys.O);
    
    P = new(this, Keys.P);
    
    Q = new(this, Keys.Q);
    
    R = new(this, Keys.R);
    
    S = new(this, Keys.S);
    
    T = new(this, Keys.T);
    
    U = new(this, Keys.U);
    
    V = new(this, Keys.V);
    
    W = new(this, Keys.W);
    
    X = new(this, Keys.X);
    
    Y = new(this, Keys.Y);
    
    Z = new(this, Keys.Z);
    
    LeftWindows = new(this, Keys.LeftWindows);
    
    RightWindows = new(this, Keys.RightWindows);
    
    NumPad0 = new(this, Keys.NumPad0);
    
    NumPad1 = new(this, Keys.NumPad1);
    
    NumPad2 = new(this, Keys.NumPad2);
    
    NumPad3 = new(this, Keys.NumPad3);
    
    NumPad4 = new(this, Keys.NumPad4);
    
    NumPad5 = new(this, Keys.NumPad5);
    
    NumPad6 = new(this, Keys.NumPad6);
    
    NumPad7 = new(this, Keys.NumPad7);
    
    NumPad8 = new(this, Keys.NumPad8);
    
    NumPad9 = new(this, Keys.NumPad9);
    
    Multiply = new(this, Keys.Multiply);
    
    Add = new(this, Keys.Add);
    
    Subtract = new(this, Keys.Subtract);
    
    Decimal = new(this, Keys.Decimal);
    
    Divide = new(this, Keys.Divide);
    
    F1 = new(this, Keys.F1);
    
    F2 = new(this, Keys.F2);
    
    F3 = new(this, Keys.F3);
    
    F4 = new(this, Keys.F4);
    
    F5 = new(this, Keys.F5);
    
    F6 = new(this, Keys.F6);
    
    F7 = new(this, Keys.F7);
    
    F8 = new(this, Keys.F8);
    
    F9 = new(this, Keys.F9);
    
    F10 = new(this, Keys.F10);
    
    F11 = new(this, Keys.F11);
    
    F12 = new(this, Keys.F12);
    
    F13 = new(this, Keys.F13);
    
    F14 = new(this, Keys.F14);
    
    F15 = new(this, Keys.F15);
    
    F16 = new(this, Keys.F16);
    
    NumLock = new(this, Keys.NumLock);
    
    Scroll = new(this, Keys.Scroll);
    
    LeftShift = new(this, Keys.LeftShift);
    
    RightShift = new(this, Keys.RightShift);
    
    LeftControl = new(this, Keys.LeftControl);
    
    RightControl = new(this, Keys.RightControl);
    
    LeftAlt = new(this, Keys.LeftAlt);
    
    RightAlt = new(this, Keys.RightAlt);
    
    }

    public IEnumerable<IComponent> Children => [
    this.Back
    
    ,this.Tab
    
    ,this.Enter
    
    ,this.CapsLock
    
    ,this.Escape
    
    ,this.Space
    
    ,this.PageUp
    
    ,this.PageDown
    
    ,this.End
    
    ,this.Home
    
    ,this.Left
    
    ,this.Up
    
    ,this.Right
    
    ,this.Down
    
    ,this.PrintScreen
    
    ,this.Insert
    
    ,this.Delete
    
    ,this.A
    
    ,this.B
    
    ,this.C
    
    ,this.D
    
    ,this.E
    
    ,this.F
    
    ,this.G
    
    ,this.H
    
    ,this.I
    
    ,this.J
    
    ,this.K
    
    ,this.L
    
    ,this.M
    
    ,this.N
    
    ,this.O
    
    ,this.P
    
    ,this.Q
    
    ,this.R
    
    ,this.S
    
    ,this.T
    
    ,this.U
    
    ,this.V
    
    ,this.W
    
    ,this.X
    
    ,this.Y
    
    ,this.Z
    
    ,this.LeftWindows
    
    ,this.RightWindows
    
    ,this.NumPad0
    
    ,this.NumPad1
    
    ,this.NumPad2
    
    ,this.NumPad3
    
    ,this.NumPad4
    
    ,this.NumPad5
    
    ,this.NumPad6
    
    ,this.NumPad7
    
    ,this.NumPad8
    
    ,this.NumPad9
    
    ,this.Multiply
    
    ,this.Add
    
    ,this.Subtract
    
    ,this.Decimal
    
    ,this.Divide
    
    ,this.F1
    
    ,this.F2
    
    ,this.F3
    
    ,this.F4
    
    ,this.F5
    
    ,this.F6
    
    ,this.F7
    
    ,this.F8
    
    ,this.F9
    
    ,this.F10
    
    ,this.F11
    
    ,this.F12
    
    ,this.F13
    
    ,this.F14
    
    ,this.F15
    
    ,this.F16
    
    ,this.NumLock
    
    ,this.Scroll
    
    ,this.LeftShift
    
    ,this.RightShift
    
    ,this.LeftControl
    
    ,this.RightControl
    
    ,this.LeftAlt
    
    ,this.RightAlt
    
    ];
}