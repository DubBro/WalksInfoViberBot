CREATE PROCEDURE Select10LongestWalks
	@imei VARCHAR(50)
AS
BEGIN
	EXECUTE dbo.GetWalks @imei;

	SELECT TOP(10)
		CAST((Distance / 1000) AS DECIMAL(18, 2)) AS DistanceInKm,
		CAST((DATEDIFF(SECOND, '00:00:00', Duration) / 60.0) AS DECIMAL(18, 2)) AS DurationInMin
	FROM dbo.Walk
	ORDER BY Distance DESC;

	DROP TABLE dbo.Walk;
END
GO