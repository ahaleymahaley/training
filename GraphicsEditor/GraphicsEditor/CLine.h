#pragma once
#include <vector>

class CLine
{
private:
	std::vector<CPoint> m_LinePoints;
	int nWidth;
	COLORREF crColor;
public:
	CLine(std::vector<CPoint> v, int w, COLORREF c)
	{
		m_LinePoints = v;
		nWidth = w;
		crColor = c;
	}
	std::vector<CPoint>& GetLinePoints()
	{
		return m_LinePoints;
	}
	int GetWidth()
	{
		return nWidth;
	}
	COLORREF GetColor()
	{
		return crColor;
	}
};

