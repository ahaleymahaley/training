// CDlg2.cpp: файл реализации
//

#include "pch.h"
#include "GraphicsEditor.h"
#include "CDlg2.h"
#include "afxdialogex.h"


// Диалоговое окно CDlg2

IMPLEMENT_DYNAMIC(CDlg2, CDialogEx)

CDlg2::CDlg2(CWnd* pParent /*=nullptr*/)
	: CDialogEx(IDD_DIALOG2, pParent)
{

}

COLORREF CDlg2::GetContent()
{
	return content;
}

CDlg2::~CDlg2()
{
}

void CDlg2::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(CDlg2, CDialogEx)
	ON_BN_CLICKED(IDOK, &CDlg2::OnBnClickedOk)
END_MESSAGE_MAP()


// Обработчики сообщений CDlg2




void CDlg2::OnBnClickedOk()
{
	// TODO: добавьте свой код обработчика уведомлений
	CDialogEx::OnOK();
	CString r, g, b;
	((CStatic*)GetDlgItem(IDC_EDIT1))->GetWindowTextW(r);
	((CStatic*)GetDlgItem(IDC_EDIT2))->GetWindowTextW(g);
	((CStatic*)GetDlgItem(IDC_EDIT3))->GetWindowTextW(b);
	content = RGB(StrToInt(r), StrToInt(g), StrToInt(b));
}
