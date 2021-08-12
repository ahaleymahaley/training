
// GraphicsEditorView.cpp: реализация класса CGraphicsEditorView
//

#include "pch.h"
#include "framework.h"
// SHARED_HANDLERS можно определить в обработчиках фильтров просмотра реализации проекта ATL, эскизов
// и поиска; позволяет совместно использовать код документа в данным проекте.
#ifndef SHARED_HANDLERS
#include "GraphicsEditor.h"
#endif

#include "GraphicsEditorDoc.h"
#include "GraphicsEditorView.h"
#include "CDlg1.h"
#include "CDlg2.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CGraphicsEditorView

IMPLEMENT_DYNCREATE(CGraphicsEditorView, CView)

BEGIN_MESSAGE_MAP(CGraphicsEditorView, CView)
	ON_WM_LBUTTONDOWN()
	ON_WM_LBUTTONUP()
	ON_WM_MOUSEMOVE()
	ON_COMMAND(ID_EDIT_UNDO, &CGraphicsEditorView::OnEditUndo)
	ON_COMMAND(ID_32771, &CGraphicsEditorView::On32771)
	ON_COMMAND(ID_32772, &CGraphicsEditorView::On32772)
	ON_WM_SIZE()
	ON_WM_SIZING()
END_MESSAGE_MAP()

// Создание или уничтожение CGraphicsEditorView

CGraphicsEditorView::CGraphicsEditorView() noexcept
{
	// TODO: добавьте код создания

}

CGraphicsEditorView::~CGraphicsEditorView()
{
}

BOOL CGraphicsEditorView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: изменить класс Window или стили посредством изменения
	//  CREATESTRUCT cs

	return CView::PreCreateWindow(cs);
}

// Рисование CGraphicsEditorView

void CGraphicsEditorView::OnDraw(CDC* pDC)
{
	CGraphicsEditorDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	if (!pDoc)
		return;
	std::vector<CLine>& lines = pDoc->GetLines();	
	int sizeLines = lines.size();
	//Запоминаем стандартный карандаш
	CPen* pPen = pDC->GetCurrentPen();
	if (sizeLines > 0)
	{
		for (int j = 0; j < sizeLines; j++)
		{
			CPen pen(PS_SOLID, lines[j].GetWidth(), lines[j].GetColor());
			//Устанавливаем свой карандаш
			pDC->SelectObject(&pen);
			std::vector<CPoint> linePoints = lines[j].GetLinePoints();
			if (linePoints.size() > 0)
			{
				pDC->MoveTo(linePoints[0]);
				int sizeLinePoints = linePoints.size();
				for (int i = 0; i < sizeLinePoints; i++)
				{
					pDC->LineTo(linePoints[i]);
				}
			}
			pDoc->AddOldLine(lines[j]);
		}
	}
	lines.clear();
	std::vector<CPoint> linePoints = pDoc->GetTempLinePoints();
	CPen pen(PS_SOLID, pDoc->nWidth, pDoc->crColor);
	pDC->SelectObject(&pen);
	int sizeLinePoints = linePoints.size();
	if (sizeLinePoints > 0) 
	{
		pDC->MoveTo(linePoints[0]);
		for (int i = 0; i < sizeLinePoints; i++)
		{
			pDC->LineTo(linePoints[i]);
		}
	}
	//Восстанавливаем контекстное устройство возвратом стандартного карандаша
	pDC->SelectObject(pPen);
	//Освобождаем контекстное устройство
	ReleaseDC(pDC);
	// TODO: добавьте здесь код отрисовки для собственных данных
}


// Диагностика CGraphicsEditorView

#ifdef _DEBUG
void CGraphicsEditorView::AssertValid() const
{
	CView::AssertValid();
}

void CGraphicsEditorView::Dump(CDumpContext& dc) const
{
	CView::Dump(dc);
}

CGraphicsEditorDoc* CGraphicsEditorView::GetDocument() const // встроена неотлаженная версия
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CGraphicsEditorDoc)));
	return (CGraphicsEditorDoc*)m_pDocument;
}
#endif //_DEBUG


// Обработчики сообщений CGraphicsEditorView


void CGraphicsEditorView::OnLButtonDown(UINT nFlags, CPoint point)
{
	// TODO: добавьте свой код обработчика сообщений или вызов стандартного
	CGraphicsEditorDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	if (pDoc)
	{
		//Начало движения мыши
		m_MouseDragging = true;
		//Добавление точки в линию
		pDoc->AddLinePoint(point);		
	}
	
	CView::OnLButtonDown(nFlags, point);
}


void CGraphicsEditorView::OnLButtonUp(UINT nFlags, CPoint point)
{
	// TODO: добавьте свой код обработчика сообщений или вызов стандартного

	//Конец движения мыши
	m_MouseDragging = false;

	CGraphicsEditorDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	if (pDoc)
	{
		CLine line(pDoc->GetTempLinePoints(), pDoc->nWidth, pDoc->crColor);
		pDoc->AddLine(line);
		pDoc->ClearTemp();
	}

	//Отрисовка
	Invalidate(false);
	UpdateWindow();

	CView::OnLButtonUp(nFlags, point);
}


void CGraphicsEditorView::OnMouseMove(UINT nFlags, CPoint point)
{
	// TODO: добавьте свой код обработчика сообщений или вызов стандартного

	if (m_MouseDragging)
	{
		CGraphicsEditorDoc* pDoc = GetDocument();
		ASSERT_VALID(pDoc);
		if (pDoc)
		{
			//Добавление точек в линию
			pDoc->AddLinePoint(point);				
		}
		//Отрисовка
		Invalidate(false);
		UpdateWindow();
	}
		
	CView::OnMouseMove(nFlags, point);
}


void CGraphicsEditorView::OnEditUndo()
{
	// TODO: добавьте свой код обработчика команд
	CGraphicsEditorDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	if (!pDoc)
		return;
	if (pDoc->GetOldLines().size() > 0)
	{
		pDoc->Undo();
	}
	Redraw();
}


void CGraphicsEditorView::On32771()
{
	// TODO: добавьте свой код обработчика команд
	CDlg1 dlg;
	
	if (dlg.DoModal() == IDOK)
	{
		GetDocument()->nWidth = dlg.GetContent();
	}

}


void CGraphicsEditorView::On32772()
{
	// TODO: добавьте свой код обработчика команд
	CDlg2 dlg;

	if (dlg.DoModal() == IDOK)
	{		
		GetDocument()->crColor = dlg.GetContent();
	}

}


void CGraphicsEditorView::OnSize(UINT nType, int cx, int cy)
{
	CView::OnSize(nType, cx, cy);
	
	// TODO: добавьте свой код обработчика сообщений
	Redraw();
}


void CGraphicsEditorView::OnSizing(UINT fwSide, LPRECT pRect)
{
	CView::OnSizing(fwSide, pRect);	

	// TODO: добавьте свой код обработчика сообщений
	Redraw();
}

void CGraphicsEditorView::Redraw()
{
	CGraphicsEditorDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	if (!pDoc)
		return;
	for each (CLine line in pDoc->GetOldLines())
	{
		pDoc->AddLine(line);
	}
	pDoc->GetOldLines().clear();
	//Отрисовка
	Invalidate();
	UpdateWindow();
}
