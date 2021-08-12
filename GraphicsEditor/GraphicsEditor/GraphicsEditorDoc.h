
// GraphicsEditorDoc.h: интерфейс класса CGraphicsEditorDoc 
//


#pragma once
#include <vector>
#include "CLine.h"


class CGraphicsEditorDoc : public CDocument
{
protected: // создать только из сериализации
	CGraphicsEditorDoc() noexcept;
	DECLARE_DYNCREATE(CGraphicsEditorDoc)

	// Атрибуты
public:

	// Операции
public:

	// Переопределение
public:
	virtual BOOL OnNewDocument();
	virtual void Serialize(CArchive& ar);
#ifdef SHARED_HANDLERS
	virtual void InitializeSearchContent();
	virtual void OnDrawThumbnail(CDC& dc, LPRECT lprcBounds);
#endif // SHARED_HANDLERS

	// Реализация
public:
	virtual ~CGraphicsEditorDoc();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif
	static int nWidth;
	static COLORREF crColor;
	std::vector<CLine>& GetLines();
	std::vector<CLine>& GetOldLines();
	std::vector<CPoint>& GetTempLinePoints();
	void AddLinePoint(const CPoint& point);
	void AddLine(const CLine& line);
	void AddOldLine(const CLine& line);
	void ClearTemp();
	void Undo();
protected:	
	std::vector<CLine> m_Lines;
	std::vector<CLine> m_oldLines;
	std::vector<CPoint> m_tempLinePoints;
// Созданные функции схемы сообщений
protected:
	DECLARE_MESSAGE_MAP()

#ifdef SHARED_HANDLERS
	// Вспомогательная функция, задающая содержимое поиска для обработчика поиска
	void SetSearchContent(const CString& value);
#endif // SHARED_HANDLERS
public:
	virtual BOOL OnOpenDocument(LPCTSTR lpszPathName);
	afx_msg void OnEditUndo();
};
