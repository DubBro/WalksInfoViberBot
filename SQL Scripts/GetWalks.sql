CREATE PROCEDURE GetWalks
	@imei VARCHAR(50)
AS
BEGIN
	CREATE TABLE TrackLocationByImei (
		Id INT IDENTITY(1, 1) PRIMARY KEY,
		TrackLocationId INT NOT NULL,
		IMEI VARCHAR(50) NOT NULL,
		Latitude DECIMAL(12, 9) NOT NULL,
		Longitude DECIMAL(12, 9) NOT NULL,
		DateTrack DATETIME NOT NULL
	);

	INSERT INTO dbo.TrackLocationByImei (TrackLocationId, IMEI, Latitude, Longitude, DateTrack) 
		SELECT id, IMEI, latitude, longitude, date_track
		FROM dbo.TrackLocation
		WHERE IMEI = @imei
		ORDER BY date_track ASC;

	DECLARE @startLocationId INT, @endLocationId INT, @startLocationTime DATETIME, @endLocationTime DATETIME, @lastLocationTime DATETIME;

	SELECT @startLocationId = MIN(Id) + 1,
		@endLocationId = MAX(Id),
		@startLocationTime = MIN(DateTrack),
		@lastLocationTime = MIN(DateTrack)
	FROM dbo.TrackLocationByImei;

	DECLARE @startPoint GEOGRAPHY, @endPoint GEOGRAPHY, @distance FLOAT = 0;

	SELECT @startPoint = GEOGRAPHY::Point(Latitude, Longitude, 4326)
	FROM dbo.TrackLocationByImei
	WHERE Id = @startLocationId - 1;

	CREATE TABLE Walk (
		Id INT IDENTITY(1, 1) PRIMARY KEY,
		Distance FLOAT NOT NULL,
		Duration TIME(3) NOT NULL
	);

	WHILE (@startLocationId <= @endLocationId)
	BEGIN
		SELECT @endLocationTime = DateTrack,
			@endPoint = GEOGRAPHY::Point(Latitude, Longitude, 4326)
		FROM dbo.TrackLocationByImei
		WHERE Id = @startLocationId;

		IF (DATEDIFF(SECOND, @lastLocationTime, @endLocationTime) >= 1800)
		BEGIN
			IF (DATEDIFF(SECOND, @startLocationTime, @lastLocationTime) != 0 AND @distance != 0)
				INSERT INTO dbo.Walk (Distance, Duration) VALUES (@distance, CAST(@lastLocationTime - @startLocationTime AS TIME));

			SET @startLocationTime = @endLocationTime;
			SET @lastLocationTime = @endLocationTime;
			SET @distance = 0;
			SET @startPoint = @endPoint;
			SET @startLocationId = @startLocationId + 1;
			CONTINUE;
		END

		IF (@startLocationId = @endLocationId AND DATEDIFF(SECOND, @startLocationTime, @endLocationTime) != 0 AND @distance != 0)
		BEGIN
			INSERT INTO dbo.Walk (Distance, Duration) VALUES (@distance, CAST(@endLocationTime - @startLocationTime AS TIME));
			SET @startLocationId = @startLocationId + 1;
			CONTINUE;
		END

		SET @distance = @distance + @startPoint.STDistance(@endPoint);
		SET @startPoint = @endPoint;
		SET @lastLocationTime = @endLocationTime;
		SET @startLocationId = @startLocationId + 1;
	END

	DROP TABLE dbo.TrackLocationByImei;
END
GO