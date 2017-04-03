--DECLARE @asAt DATE = null --CONVERT(DATE, '20160101',112)

DECLARE @dt DATE = @asAt

if @dt is null

	SELECT COUNT(*) as initiativeCount
	FROM Initiative
else
	SELECT COUNT(*) as initiativeCount
	FROM Initiative
	WHERE StartDate <= @dt 
	AND   EndDate >= @dt

