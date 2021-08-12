
// GraphicsEditorDoc.cpp: реализация класса CGraphicsEditorDoc 
//

#include "pch.h"
#include "framework.h"
// SHARED_HANDLERS можно определить в обработчиках фильтров просмотра реализации проекта ATL, эскизов
// и поиска; позволяет совместно использовать код документа в данным проекте.
#ifndef SHARED_HANDLERS
#include "GraphicsEditor.h"
#endif

#include "GraphicsEditorDoc.h"

#include <propkey.h>

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

// CGraphicsEditorDoc

IMPLEMENT_DYNCREATE(CGraphicsEditorDoc, CDocument)

BEGIN_MESSAGE_MAP(CGraphicsEditorDoc, CDocument)
	ON_COMMAND(ID_EDIT_UNDO, &CGraphicsEditorDoc::OnEditUndo)
END_MESSAGE_MAP()


// Создание или уничтожение CGraphicsEditorDoc

CGraphicsEditorDoc::CGraphicsEditorDoc() noexcept
{
	// TODO: добавьте код для одноразового вызова конструктора

}

CGraphicsEditorDoc::~CGraphicsEditorDoc()
{
}

BOOL CGraphicsEditorDoc::OnNewDocument()
{
	if (!CDocument::OnNewDocument())
		return FALSE;

	// TODO: добавьте код повторной инициализации
	// (Документы SDI будут повторно использовать этот документ)
	m_Lines.clear();
	m_oldLines.clear();

	return TRUE;
}




// Сериализация CGraphicsEditorDoc

void CGraphicsEditorDoc::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{
		// TODO: добавьте код сохранения
		int sizej = m_oldLines.size();
		ar << sizej;
		for (int j = 0; j < sizej; j++)
		{
			CLine line = m_oldLines[j];
			int sizei = line.GetLinePoints().size();
			ar << sizei;
			for (int i = 0; i < sizei; i++)
			{
				CPoint point = line.GetLinePoints()[i];
				ar << point.x << point.y;
			}			
			int r = GetRValue(line.GetColor()), g = GetGValue(line.GetColor()), b = GetBValue(line.GetColor());
			ar << line.GetWidth() << r << g << b;
		}
	}
	else
	{
		// TODO: добавьте код загрузки
		int sizej;
		ar >> sizej;
		for (int j = 0; j < sizej; j++)
		{
			int sizei;
			ar >> sizei;
			CPoint point;
			for (int i = 0; i < sizei; i++)
			{
				ar >> point.x >> point.y;
				m_tempLinePoints.push_back(point);
			}
			int w, r, g, b;
			ar >> w >> r >> g >> b;
			CLine line(m_tempLinePoints, w, RGB(r,g,b));
			this->AddLine(line);	
			this->ClearTemp();
		}
	}
}

#ifdef SHARED_HANDLERS

// Поддержка для эскизов
void CGraphicsEditorDoc::OnDrawThumbnail(CDC& dc, LPRECT lprcBounds)
{
	// Измените этот код для отображения данных документа
	dc.FillSolidRect(lprcBounds, RGB(255, 255, 255));

	CString strText = _T("TODO: implement thumbnail drawing here");
	LOGFONT lf;

	CFont* pDefaultGUIFont = CFont::FromHandle((HFONT) GetStockObject(DEFAULT_GUI_FONT));
	pDefaultGUIFont->GetLogFont(&lf);
	lf.lfHeight = 36;

	CFont fontDraw;
	fontDraw.CreateFontIndirect(&lf);

	CFont* pOldFont = dc.SelectObject(&fontDraw);
	dc.DrawText(strText, lprcBounds, DT_CENTER | DT_WORDBREAK);
	dc.SelectObject(pOldFont);
}

// Поддержка обработчиков поиска
void CGraphicsEditorDoc::InitializeSearchContent()
{
	CString strSearchContent;
	// Задание содержимого поиска из данных документа.
	// Части содержимого должны разделяться точкой с запятой ";"

	// Например:  strSearchContent = _T("точка;прямоугольник;круг;объект ole;");
	SetSearchContent(strSearchContent);
}

void CGraphicsEditorDoc::SetSearchContent(const CString& value)
{
	if (value.IsEmpty())
	{
		RemoveChunk(PKEY_Search_Contents.fmtid, PKEY_Search_Contents.pid);
	}
	else
	{
		CMFCFilterChunkValueImpl *pChunk = nullptr;
		ATLTRY(pChunk = new CMFCFilterChunkValueImpl);
		if (pChunk != nullptr)
		{
			pChunk->SetTextValue(PKEY_Search_Contents, value, CHUNK_TEXT);
			SetChunkValue(pChunk);
		}
	}
}

#endif // SHARED_HANDLERS

// Диагностика CGraphicsEditorDoc

#ifdef _DEBUG
void CGraphicsEditorDoc::AssertValid() const
{
	CDocument::AssertValid();
}

void CGraphicsEditorDoc::Dump(CDumpContext& dc) const
{
	CDocument::Dump(dc);
}
std::vector<CLine>& CGraphicsEditorDoc::GetLines()
{
	// TODO: вставьте здесь оператор return
	return m_Lines;
}
std::vector<CLine>& CGraphicsEditorDoc::GetOldLines()
{
	// TODO: вставьте здесь оператор return
	return m_oldLines;
}
std::vector<CPoint>& CGraphicsEditorDoc::GetTempLinePoints()
{
	// TODO: вставьте здесь оператор return
	return m_tempLinePoints;
}
void CGraphicsEditorDoc::AddLinePoint(const CPoint& point)
{
	m_tempLinePoints.push_back(point);
}
void CGraphicsEditorDoc::AddLine(const CLine& line)
{
	m_Lines.push_back(line);
}
void CGraphicsEditorDoc::AddOldLine(const CLine& line)
{
	m_oldLines.push_back(line);
}
void CGraphicsEditorDoc::ClearTemp()
{
	m_tempLinePoints.clear();
}
void CGraphicsEditorDoc::Undo()
{
	m_oldLines.pop_back();
}
#endif //_DEBUG


// Команды CGraphicsEditorDoc


BOOL CGraphicsEditorDoc::OnOpenDocument(LPCTSTR lpszPathName)
{
	m_Lines.clear();
	m_oldLines.clear();

	if (!CDocument::OnOpenDocument(lpszPathName))
		return FALSE;

	// TODO:  Добавьте специализированный код создания

	return TRUE;
}

int CGraphicsEditorDoc::nWidth = 1;
COLORREF CGraphicsEditorDoc::crColor = RGB(0, 0, 0);



void CGraphicsEditorDoc::OnEditUndo()
{
	// TODO: добавьте свой код обработчика команд
}
