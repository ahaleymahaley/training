#pragma once


// Диалоговое окно CDlg1

class CDlg1 : public CDialog
{
	DECLARE_DYNAMIC(CDlg1)
private:
	int content;

public:
	CDlg1(CWnd* pParent = nullptr);   // стандартный конструктор
	int GetContent();
	virtual ~CDlg1();

// Данные диалогового окна
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_DIALOG1 };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // поддержка DDX/DDV

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedOk();
};
