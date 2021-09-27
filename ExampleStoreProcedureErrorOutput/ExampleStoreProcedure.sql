-- ========================================================================================
-- Description: Beispiel einer Store Procedure, mit de absichtlich ein 
--              Ausnahmefehler erzeugt werden kann.
--              Script Inhalt für die Ziel MS SQL Datenbank.
-- ========================================================================================

USE [example-data]                                          -- Datenbank Name. Ggf. 
                                                            -- anpassen für eigene Datenbank Umgebung
GO
SET Ansi_Nulls ON
GO
CREATE PROCEDURE [dbo].[myStoreProcedure]                   -- Name der gespeicherten Prozedure
    @showMeErrorMessage AS BIT,                             -- 0 für Normalen durchlauf, um den Status Text zu erhalten
                                                            -- 1 für das werfen eines Ausnahmefehler
    @myGetStatusMessage AS NVARCHAR(20) OUTPUT,             -- Gibt einen Status Text zurück
    @myStoreProcedureErrorMessage NVARCHAR(100) OUTPUT      -- Gibt den Fehlerbericht zurück
AS
Begin Try
    IF @showMeErrorMessage = 1
    Begin
        THROW 123, 'Eine Ausnahme wurde geworfen', 1;       -- Ausnahmefehler werfen
    end
    
    SELECT @myGetStatusMessage = 'Alles in Ordnung!'        -- Ausgabe des Status an den Output weiter geben.
end try
begin catch
    SELECT @myStoreProcedureErrorMessage = ERROR_MESSAGE(); -- Ausgabe des gesamten Fehlerbericht
                                                            -- an den Declarierten Output Variable.
end catch