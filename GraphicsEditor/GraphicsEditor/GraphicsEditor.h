
// GraphicsEditor.h: основной файл заголовка для приложения GraphicsEditor
//
#pragma once

#ifndef __AFXWIN_H__
	#error "включить pch.h до включения этого файла в PCH"
#endif

#include "resource.h"       // основные символы


// CGraphicsEditorApp:
// Сведения о реализации этого класса: GraphicsEditor.cpp
//

class CGraphicsEditorApp : public CWinApp
{
public:
	CGraphicsEditorApp() noexcept;


// Переопределение
public:
	virtual BOOL InitInstance();

// Реализация
	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern CGraphicsEditorApp theApp;
