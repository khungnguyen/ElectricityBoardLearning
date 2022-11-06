
using System.Collections.Generic;
[System.Serializable]
public class JCircuitBoardDatabase
{
    public JCircuitBoardItem[] CircuitBoards;
}
[System.Serializable]
public class JCircuitBoardItem
{
    public string name;
    public string model;
    public string id;
    public JCircuitBoardPractice[] practices;

    public JCircuitBoardPractice GetPracticeById(int id)
    {
        return new List<JCircuitBoardPractice>(practices).Find(e => e.id == id);
    }
}
[System.Serializable]
public class JCircuitBoardPractice
{
    public string name;
    public string instruction;
    public int id;

    public JPracticeStep[] CorrectSteps;
    public JPracticeStep[] DefaultBoard;

    public List<JPracticeStep> GetCorrectSteps() {
        return new List<JPracticeStep>(CorrectSteps);
    }
    public List<JPracticeStep> GetDefaultItemStatus() {
        return new List<JPracticeStep>(DefaultBoard);
    }
}
[System.Serializable]
public class JPracticeStep
{
    public string type;
    public string value;
}

public class JPracticeHolder
{
    private JCircuitBoardItem board;
    private int practiceID;

    public JCircuitBoardItem GetBoard()
    {
        return board;
    }
    public int GetPracticeId()
    {
        return practiceID;
    }
    public JPracticeHolder(JCircuitBoardItem board, int practiceID)
    {
        this.board = board;
        this.practiceID = practiceID;
    }
}