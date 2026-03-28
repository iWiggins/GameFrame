# GameFrame

A monogame framework for quickly building dynamic 2D games using an extensible component library.

## Examples

[Pong](https://github.com/iWiggins/Pong)

## How it Works

GameFrame works by automating the work of building a UI, checking behavioral states, and updating game objects using a system of Frames and Components.

### Frames

A frame is a major unit of function, such as a menu or the core gameplay. Frames are built from components, and automate the component behavior.

### Components

Every game object is a component. Each component contains logic for its operation, how it behaves, how it interacts, and what it looks like. Components contain child components to build up complex functionality from individual parts.

All components are ancestors of the root component of a frame. The frame then automates the behavior of all of the components it contains.

### Layouts

Layouts are special components which dynamically arrange child components on the screen, eliminating the need for hand-written UI code.

### Input

Input can be handled from within a frame by accessing the Keyboard and Mouse, and by using clickable components. Input is handled using C# events to respond to input events like a click, a hover, or a keypress.

### Library

GameFrame comes with a library of common components like buttons and autosizing text to ease UI construction.

## Documentation

[gameframe.ianw.io](https://gameframe.ianw.io)