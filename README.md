# Calculator Logger
A project is .NET library for logging mathematical operations and calculations. Supported mathematical operations are sum, multiplication, subtraction, division. Supported statistical operations are get minimum, maximum and average value among given numbers. Supported logic is resolving single and multi conditions, switches, bands.
## Getting Started
The project was created in Visual Studio Community 2015 using .NET Framework 4.6.1. In order to create a calculator that logs all supported calculations, your class should implement an ICalculator interface. Implementation of the ICalculator interface requires to have a property of IFormulaLogger type. So, we can say that whole logic is contained in 2 objects of ICalculator and IFormulaLogger types. ICalculator is responsible for calculations, on the other hand IFormulaLogger is responsible for logging of mathematical operations.
## Running the tests
A code includes series of unit tests created in Visual Studio.
## Versioning
Release sequence, release time and version number are defined by author. 
## Author
Andriy Shyrokoryadov.
## License
This project is licensed under the MIT License - see the LICENSE.md file for details.
## Hints
### Implementations namespace
A namespace where yoiu can find one implementation of ICalculator interface (StandardCalculator) and 2 implementations of IFormulaLogger interface (ConsoleFormulaLogger and XMLFormulaLogger).
### Interfaces namespace
Except several interfaces already mentioned, a few more interface defined here. Basically the IFormulaLogger interface combines other small interface in one generic interface.
### Models namespace
Models used in a librabry defined here.
### Static namespace
Helper static classes defined here.
### CalculatorLoggerApp
A sample application showing how library could be used.
