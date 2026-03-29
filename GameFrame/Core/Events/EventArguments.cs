using GameFrame.Core.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.Core.Events;

public record MouseDownArgs(Mouse.Buttons Button, Point Position);

public record MouseUpArgs(Mouse.Buttons Button, Point Position, double Duration);

public record MouseHoverArgs(Point Position);

public record MouseUnhoverArgs(double Duration);