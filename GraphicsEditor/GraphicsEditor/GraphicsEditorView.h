
// GraphicsEditorView.h: интерфейс класса CGraphicsEditorView
//

#pragma once


class CGraphicsEditorView : public CView
{
protected: // создать только из сериализации
	CGraphicsEditorView() noexcept;
	DECLARE_DYNCREATE(CGraphicsEditorView)

// Атрибуты
public:
	CGraphicsEditorDoc* GetDocument() const;
// Операции
public:
// Переопределение
public:
	virtual void OnDraw(CDC* pDC);  // переопределено для отрисовки этого представления
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
protected:

// Реализация
public:
	virtual ~CGraphicsEditorView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:
	bool m_MouseDragging{ false };
// Созданные функции схемы сообщений
protected:
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnLButtonDown(UINT nFlags, CPoint point);
	afx_msg void OnLButtonUp(UINT nFlags, CPoint point);
	afx_msg void OnMouseMove(UINT nFlags, CPoint point);
	afx_msg void OnEditUndo();
	afx_msg void On32771();
	afx_msg void On32772();
	afx_msg void OnSize(UINT nType, int cx, int cy);
	afx_msg void OnSizing(UINT fwSide, LPRECT pRect);
	void Redraw();
};

#ifndef _DEBUG  // версия отладки в GraphicsEditorView.cpp
inline CGraphicsEditorDoc* CGraphicsEditorView::GetDocument() const
   { return reinterpret_cast<CGraphicsEditorDoc*>(m_pDocument); }
#endif

