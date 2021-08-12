// CDlg1.cpp: файл реализации
//

#include "pch.h"
#include "GraphicsEditor.h"
#include "CDlg1.h"
#include "afxdialogex.h"


// Диалоговое окно CDlg1

IMPLEMENT_DYNAMIC(CDlg1, CDialog)

CDlg1::CDlg1(CWnd* pParent /*=nullptr*/)
	: CDialog(IDD_DIALOG1, pParent)
{

}

int CDlg1::GetContent()
{
	return content;
}

CDlg1::~CDlg1()
{
}

void CDlg1::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(CDlg1, CDialog)
	ON_BN_CLICKED(IDOK, &CDlg1::OnBnClickedOk)
END_MESSAGE_MAP()


// Обработчики сообщений CDlg1



void CDlg1::OnBnClickedOk()
{
	// TODO: добавьте свой код обработчика уведомлений
	CDialog::OnOK();
	CString t;
	((CEdit*)GetDlgItem(IDC_EDIT1))->GetWindowTextW(t);
	content = StrToInt(t);
}
