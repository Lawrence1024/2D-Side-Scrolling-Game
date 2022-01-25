//Move the qActivators to the previous qActivator's position
	Debug.Log("called");
	if (!positionToggle)
	{
		positionToggle = true;
		pos.x = gCanvas.GetComponent<qActivatorPos>().positions[qActivatorCount-1][0];
		pos.y = gCanvas.GetComponent<qActivatorPos>().positions[qActivatorCount-1][1];
		
	}
	else
	{
		positionToggle = false;
		pos.x = gCanvas.GetComponent<qActivatorPos>().positions[qActivatorCount][0];
		pos.y = gCanvas.GetComponent<qActivatorPos>().positions[qActivatorCount][1];
	}
//Sliders
	public void matchSlider(float value) {
        sliderValue.transform.GetComponent<TMP_InputField>().text = "" + value;
    }
//Canvas Positioning
	private void SetTop(RectTransform rt, float top)
	{
		rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
	}
	
	private void SetBottom(RectTransform rt, float bottom)
	{
		rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
	}