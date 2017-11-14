public class Record  {
	private string name;
	private string playerStatus;
	private string objectStatus;
	private int count;

	public Record(string name, string playerStatus, string objectStatus) {
		this.name = name;
		this.playerStatus = playerStatus;
		this.objectStatus = objectStatus;
		this.count = 0;
	}

	public string getName() {
		return name;
	}

	public int getCount() {
		return count;
	}

	public string getObjectStatus() {
		return objectStatus;
	}

	public string getPlayerStatus() {
		return playerStatus;
	}

	public string toString() {
		return getObjectStatus () + ":" + getName () + "\n" + getPlayerStatus() + "\nCount:" + getCount() +"\n";
	}

	public void incrementCount() {
		count++;
	}

	public override bool Equals(object obj) {
		Record temp = obj as Record;
		if (obj == null) {
			return false;
		}
		if (getObjectStatus ().Equals (temp.getObjectStatus ()) && getObjectStatus ().Equals (temp.getObjectStatus ())) {
			return true;
		}
		return false;
	}
}
