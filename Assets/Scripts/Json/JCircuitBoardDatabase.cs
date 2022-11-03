
[System.Serializable]
public class JCircuitBoardDatabase 
{
   public JCircuitBoardItem[] CircuitBoards;
}
[System.Serializable]
public class JCircuitBoardItem {
    public string name;
    public string model;
    public string id;
    public JCircuitBoardPractice[] practices;
}
[System.Serializable]
public class JCircuitBoardPractice {
    public string name;
    public int id;
    public JPracticeStep[] Steps;
}
[System.Serializable]
public class JPracticeStep {
    public string type;
    public string value;
}