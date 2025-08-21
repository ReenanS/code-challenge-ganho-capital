using Domain.Entities;
using Domain.Enums;

namespace Tests.Integration.Fixtures;

public class OperationsFixture
{

    // ---------------------- Caso 1 ----------------------
    public List<Operation> Caso1 => new()
    {
        new Operation(OperationTypeEnum.Buy, 10.00m, 100),
        new Operation(OperationTypeEnum.Sell, 15.00m, 50),
        new Operation(OperationTypeEnum.Sell, 15.00m, 50)
    };
    public decimal[] Caso1Expected => new decimal[] { 0m, 0m, 0m };

    // ---------------------- Caso 2 ----------------------
    public List<Operation> Caso2 => new()
    {
        new Operation(OperationTypeEnum.Buy, 10.00m, 10000),
        new Operation(OperationTypeEnum.Sell, 20.00m, 5000),
        new Operation(OperationTypeEnum.Sell, 5.00m, 5000)
    };
    public decimal[] Caso2Expected => new decimal[] { 0m, 10000m, 0m };

    // ---------------------- Caso 3 ----------------------
    public List<Operation> Caso3 => new()
    {
        new Operation(OperationTypeEnum.Buy, 10.00m, 10000),
        new Operation(OperationTypeEnum.Sell, 5.00m, 5000),
        new Operation(OperationTypeEnum.Sell, 20.00m, 3000)
    };
    public decimal[] Caso3Expected => new decimal[] { 0m, 0m, 1000m };

    // ---------------------- Caso 4 ----------------------
    public List<Operation> Caso4 => new()
    {
        new Operation(OperationTypeEnum.Buy, 10.00m, 10000),
        new Operation(OperationTypeEnum.Buy, 25.00m, 5000),
        new Operation(OperationTypeEnum.Sell, 15.00m, 10000)
    };
    public decimal[] Caso4Expected => new decimal[] { 0m, 0m, 0m };

    // ---------------------- Caso 5 ----------------------
    public List<Operation> Caso5 => new()
    {
        new Operation(OperationTypeEnum.Buy, 10.00m, 10000),
        new Operation(OperationTypeEnum.Buy, 25.00m, 5000),
        new Operation(OperationTypeEnum.Sell, 15.00m, 10000),
        new Operation(OperationTypeEnum.Sell, 25.00m, 5000)
    };
    public decimal[] Caso5Expected => new decimal[] { 0m, 0m, 0m, 10000m };

    // ---------------------- Caso 6 ----------------------
    public List<Operation> Caso6 => new()
    {
        new Operation(OperationTypeEnum.Buy, 10.00m, 10000),
        new Operation(OperationTypeEnum.Sell, 2.00m, 5000),
        new Operation(OperationTypeEnum.Sell, 20.00m, 2000),
        new Operation(OperationTypeEnum.Sell, 20.00m, 2000),
        new Operation(OperationTypeEnum.Sell, 25.00m, 1000)
    };
    public decimal[] Caso6Expected => new decimal[] { 0m, 0m, 0m, 0m, 3000m };

    // ---------------------- Caso 7 ----------------------
    public List<Operation> Caso7 => new()
    {
        new Operation(OperationTypeEnum.Buy, 10.00m, 10000),
        new Operation(OperationTypeEnum.Sell, 2.00m, 5000),
        new Operation(OperationTypeEnum.Sell, 20.00m, 2000),
        new Operation(OperationTypeEnum.Sell, 20.00m, 2000),
        new Operation(OperationTypeEnum.Sell, 25.00m, 1000)
    };
    public decimal[] Caso7Expected => new decimal[] { 0m, 0m, 0m, 0m, 3000m };

    // ---------------------- Caso 8 ----------------------
    public List<Operation> Caso8 => new()
    {
        new Operation(OperationTypeEnum.Buy, 10.00m, 10000),
        new Operation(OperationTypeEnum.Sell, 50.00m, 10000),
        new Operation(OperationTypeEnum.Buy, 20.00m, 10000),
        new Operation(OperationTypeEnum.Sell, 50.00m, 10000)
    };
    public decimal[] Caso8Expected => new decimal[] { 0m, 80000m, 0m, 60000m };

    // ---------------------- Caso 9 ----------------------
    public List<Operation> Caso9 => new()
    {
        new Operation(OperationTypeEnum.Buy, 5000.00m, 10),
        new Operation(OperationTypeEnum.Sell, 4000.00m, 5),
        new Operation(OperationTypeEnum.Buy, 15000.00m, 5),
        new Operation(OperationTypeEnum.Buy, 4000.00m, 2),
        new Operation(OperationTypeEnum.Buy, 23000.00m, 2),
        new Operation(OperationTypeEnum.Sell, 20000.00m, 1),
        new Operation(OperationTypeEnum.Sell, 12000.00m, 10),
        new Operation(OperationTypeEnum.Sell, 15000.00m, 3)
    };
    public decimal[] Caso9Expected => new decimal[] { 0m, 0m, 0m, 0m, 0m, 0m, 1000m, 2400m };
}
