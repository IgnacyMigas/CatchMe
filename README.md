# CatchMe

Try catch wrapper. You can setup CatchMe what to handle and what to do and use it in every method.

# How to use?

Use the nuget package [![NuGet](https://img.shields.io/nuget/v/CatchMe.svg?style=flat-square)](https://www.nuget.org/packages/CatchMe/)

Add using to your file:

```csharp
using CatchMe;
```

Create you rules in the constructor like this:

```csharp
_exceptionHandler = CatchMe.Handle<NullReferenceException>(() => Console.WriteLine("Null reference exception")).Or<Exception>(() => Console.WriteLine("Generic exception"));
```

In methods run your code with exception handler

```csharp
_exceptionHandler.Execute(() => DoYourStuff());
```

# CatchMe

CatchMe is a lightweight and easy-to-use exception handling library for .NET applications. It simplifies the process of catching and handling exceptions by providing a fluent interface to specify exception types and associated action methods.

## Installation

The CatchMe library can be installed via NuGet package manager. To install the package, run the following command in the Package Manager Console:

```powershell
Install-Package CatchMe
```

## Usage

Here's an example of how to use the CatchMe library to catch and handle exceptions:

```csharp
CatchMe
    .Handle<ArgumentNullException>(e => Console.WriteLine("ArgumentNullException caught"))
    .Handle<InvalidOperationException>(e => Console.WriteLine("InvalidOperationException caught"))
    .Or<NotSupportedException>(e => Console.WriteLine("NotSupportedException caught"))
    .Finally(() => Console.WriteLine("Finally block executed"))
    .Execute(() => { /* Code that might throw an exception */ });
```

Or

```csharp
public Constructor()
{
    _exceptionHandler = CatchMe
        .Handle<ArgumentNullException>(e => Console.WriteLine("ArgumentNullException caught"))
        .Handle<InvalidOperationException>(e => Console.WriteLine("InvalidOperationException caught"))
        .Or<NotSupportedException>(e => Console.WriteLine("NotSupportedException caught"))
        .Finally(() => Console.WriteLine("Finally block executed"))
}

public void Method()
{
    _exceptionHandler.Execute(() => { /* Code that might throw an exception */ });
}
```


In the example above, we use the `Handle` method to specify exception types and associated action methods. We also use the `Or` method to handle additional exception types, and the `Finally` method to execute code after the try-catch block is finished. Finally, we call the `Execute` method to run the code and catch any exceptions.

## API Reference

The CatchMe library provides the following methods:

### `Handle<T>()`

Specifies an exception type and sets the associated action method.

### `Handle<T>(Action<Exception> action)`

Specifies an exception type and sets the associated action method.

### `Handle(Type type)`

Specifies an exception type and sets the associated action method.

### `Handle(Type type, Action<Exception> action)`

Specifies an exception type and sets the associated action method.

### `Or<T>()`

Specifies an additional exception type and sets the associated action method.

### `Or<T>(Action<Exception> action)`

Specifies an additional exception type and sets the associated action method.

### `Or(Type type)`

Specifies an additional exception type and sets the associated action method.

### `Or(Type type, Action<Exception> action)`

Specifies an additional exception type and sets the associated action method.

### `Finally(Action action)`

Specifies code to execute after the try-catch block is finished.

### `Execute(Action action)`

Executes the specified code and catches any exceptions.

### `Execute<T>(Func<T> func)`

Executes the specified code and catches any exceptions.

## Contributing

Contributions are welcome! If you find a bug or have an idea for a new feature, please open an issue on GitHub. If you would like to contribute code, please fork the repository and submit a pull request.

## License

CatchMe is licensed under the MIT License. See `LICENSE` for more information.