CREATE VIEW UserAndMoney AS

SELECT 
    u.Id AS UserId, 
    u.CreatedAt, 
    u.Username, 
    u.Email, 
    u.FullName,
	i.TotalIncome,
    e.TotalExpense
FROM dbo.TUser u
LEFT JOIN (
    SELECT UserId, SUM(IncomeMoney) AS TotalIncome
    FROM dbo.TIncome
    GROUP BY UserId
) i ON u.Id = i.UserId
LEFT JOIN (
    SELECT UserId, SUM(Cost) AS TotalExpense
    FROM dbo.TExpense
    GROUP BY UserId
) e ON u.Id = e.UserId;
