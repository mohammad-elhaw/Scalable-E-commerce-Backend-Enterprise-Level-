using Application.Abstractions.Pagination;
using Dapper;
using Inventory.Application;
using System.Text;

namespace Inventory.Queries;

public class InventoryQueries(
    ISqlConnectionFactory connectionFactory)
    : IInventoryQueries
{
    public async Task<InventoryAvailabilityReadModel?> GetAvailabilityAsync(
        Guid productVariantId,
        CancellationToken cancellationToken)
    {
        const string sql =
            """
            SELECT
                ProductVariantId,
                SUM(QuantityOnHand) AS TotalOnHand,
                SUM(ReservedQuantity) AS TotalReserved,
                SUM(QuantityOnHand - ReservedQuantity) AS TotalAvailable
            FROM InventoryItems
            WHERE ProductVariantId = @ProductVariantId
            GROUP BY ProductVariantId
            """;

        using var connection = connectionFactory.Create();

        return await connection
            .QuerySingleOrDefaultAsync<InventoryAvailabilityReadModel>(
            sql,
            new
            {
                ProductVariantId = productVariantId
            });
    }

    public async Task<InventoryItemReadModel?> GetByIdAsync(
        Guid inventoryItemId,
        CancellationToken cancellationToken)
    {
        const string sql =
            """
            SELECT
                ii.Id,
                ii.ProductVariantId,
                ii.WarehouseId,

                w.Code AS WarehouseCode,
                w.Name AS WarehouseName,

                ii.QuantityOnHand,
                ii.ReservedQuantity,
                ii.QuantityOnHand - ii.ReservedQuantity AS AvailableQuantity,

                ii.IsActive

                FROM InventoryItems ii
                INNER JOIN Warehouses w 
                    ON ii.WarehouseId = w.Id

                WHERE ii.Id = @InventoryItemId
            """;

        using var connection = connectionFactory.Create();

        return await connection
            .QuerySingleOrDefaultAsync<InventoryItemReadModel>(
            sql,
            new
            {
                InventoryItemId = inventoryItemId
            });
    }

    public async Task<InventoryItemReadModel?> GetByVariantAsync(
        Guid productVariantId, 
        Guid warehouseId, 
        CancellationToken cancellationToken)
    {
        const string sql =
            """
            SELECT
                ii.Id,
                ii.ProductVariantId,
                ii.WarehouseId,
                w.Code AS WarehouseCode,
                w.Name AS WarehouseName,
                ii.QuantityOnHand,
                ii.ReservedQuantity,
                ii.QuantityOnHand - ii.ReservedQuantity AS AvailableQuantity,
                ii.IsActive
                FROM InventoryItems ii
                INNER JOIN Warehouses w 
                    ON ii.WarehouseId = w.Id
                WHERE ii.ProductVariantId = @ProductVariantId
                    AND ii.WarehouseId = @WarehouseId
            """;

        using var connection = connectionFactory.Create();

        return await connection
            .QuerySingleOrDefaultAsync<InventoryItemReadModel>(
            sql,
            new
            {
                ProductVariantId = productVariantId,
                WarehouseId = warehouseId
            });
    }

    public async Task<PagedResult<InventoryItemReadModel>> SearchAsync(
        InventorySearchFilter filter, 
        CancellationToken cancellationToken)
    {
        var whereBuilder = new StringBuilder("WHERE 1 = 1");
        var parameters = new DynamicParameters();

        if (filter.ProductVariantId.HasValue)
        {
            whereBuilder.AppendLine(
                " AND ii.ProductVariantId = @ProductVariantId");

            parameters.Add("ProductVariantId", filter.ProductVariantId);
        }

        if (filter.WarehouseId.HasValue)
        {
            whereBuilder.AppendLine(
                " AND ii.WarehouseId = @WarehouseId");

            parameters.Add("WarehouseId", filter.WarehouseId);
        }

        if (filter.IsActive.HasValue)
        {
            whereBuilder.AppendLine(
                " AND ii.IsActive = @IsActive");

            parameters.Add("IsActive", filter.IsActive);
        }

        if (filter.MinimumAvailableQuantity.HasValue)
        {
            whereBuilder.AppendLine(
                """
                AND (ii.QuantityOnHand - ii.ReservedQuantity)
                    >= @MinimumAvailableQuantity
                """);

            parameters.Add(
                "MinimumAvailableQuantity",
                filter.MinimumAvailableQuantity);
        }

        if (filter.MaximumAvailableQuantity.HasValue)
        {
            whereBuilder.AppendLine(
                """
                AND (ii.QuantityOnHand - ii.ReservedQuantity)
                    <= @MaximumAvailableQuantity
                """);

            parameters.Add(
                "MaximumAvailableQuantity",
                filter.MaximumAvailableQuantity);
        }

        if (!string.IsNullOrWhiteSpace(filter.WarehouseCode))
        {
            whereBuilder.AppendLine(
                " AND w.Code LIKE @WarehouseCode");

            parameters.Add(
                "WarehouseCode",
                $"%{filter.WarehouseCode}%");
        }

        if (!string.IsNullOrWhiteSpace(filter.WarehouseName))
        {
            whereBuilder.AppendLine(
                " AND w.Name LIKE @WarehouseName");

            parameters.Add(
                "WarehouseName",
                $"%{filter.WarehouseName}%");
        }

        var orderBy = filter.SortBy switch
        {
            InventorySortBy.AvailableQuantity =>
                "AvailableQuantity",

            InventorySortBy.QuantityOnHand =>
                "ii.QuantityOnHand",

            InventorySortBy.ReservedQuantity =>
                "ii.ReservedQuantity",

            InventorySortBy.CreatedAt =>
                "ii.CreatedAtUtc",

            _ =>
                "w.Name"
        };

        var direction =
            filter.SortDirection == SortDirection.Desc
                ? "DESC"
                : "ASC";

        var countSql =
            $"""
            SELECT COUNT(*)
            FROM InventoryItems ii
            INNER JOIN Warehouses w
                ON w.Id = ii.WarehouseId
            {whereBuilder}
            """;

        var dataSql =
            $"""
            SELECT
                ii.Id,
                ii.ProductVariantId,
                ii.WarehouseId,

                w.Code AS WarehouseCode,
                w.Name AS WarehouseName,

                ii.QuantityOnHand,
                ii.ReservedQuantity,

                ii.QuantityOnHand - ii.ReservedQuantity
                    AS AvailableQuantity,

                ii.TrackInventory,
                ii.IsActive

                ii.CreatedAtUtc,
                ii.ModifiedAtUtc

            FROM InventoryItems ii
            INNER JOIN Warehouses w
                ON w.Id = ii.WarehouseId

            {whereBuilder}

            ORDER BY {orderBy} {direction}

            OFFSET @Offset ROWS
            FETCH NEXT @PageSize ROWS ONLY
            """;

        parameters.Add(
            "Offset",
            (filter.Page - 1) * filter.PageSize);

        parameters.Add(
            "PageSize",
            filter.PageSize);

        using var connection = connectionFactory.Create();

        var totalCount =
            await connection.ExecuteScalarAsync<int>(
                countSql,
                parameters);

        var items =
            (await connection.QueryAsync<InventoryItemReadModel>(
                dataSql,
                parameters))
            .ToList();

        return new PagedResult<InventoryItemReadModel>
        {
            Items = items,
            TotalCount = totalCount,
            Page = filter.Page,
            PageSize = filter.PageSize
        };
    }
}
