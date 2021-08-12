#pragma once

// Диалоговое окно CDlg2

class CDlg2 : public CDialogEx
{
	DECLARE_DYNAMIC(CDlg2)
private:
	COLORREF content;

public:
	CDlg2(CWnd* pParent = nullptr);   // стандартный конструктор
	COLORREF GetContent();
	virtual ~CDlg2();

// Данные диалогового окна
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_DIALOG2 };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // поддержка DDX/DDV

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedOk();
};
