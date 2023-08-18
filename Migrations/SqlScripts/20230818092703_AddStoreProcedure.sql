CREATE PROCEDURE BuyDream
    @MotivationId NVARCHAR(100),
    @UserId NVARCHAR(100),
    @ReceiptFile NVARCHAR(255) = NULL
AS
IF EXISTS (SELECT 1 FROM dbo.TMotivation WHERE Id = @MotivationId AND UserId = @UserId)
	BEGIN 
		WITH NewExpense AS 
		(
			SELECT 
				Id,
				GETUTCDATE() AS CreatedAt,
				GETUTCDATE() AS UpdatedAt,
				TargetName,
				TargetPrice,
				6 AS CostTYpe,
				UserId, 
				@ReceiptFile AS ReceiptFile
			FROM dbo.TMotivation WHERE Id = @MotivationId
		)
		
		INSERT INTO dbo.TExpense SELECT * FROM NewExpense;
	
		DELETE FROM dbo.TMotivation WHERE Id = @MotivationId;
	END;