using System;

namespace Sbn.FramWork.Windows
{
	public enum Win32Messages : uint
	{
		WM_NULL,
		WM_CREATE,
		WM_DESTROY,
		WM_MOVE,
		WM_SIZE = 5u,
		WM_ACTIVATE,
		WM_SETFOCUS,
		WM_KILLFOCUS,
		WM_ENABLE = 10u,
		WM_SETREDRAW,
		WM_SETTEXT,
		WM_GETTEXT,
		WM_GETTEXTLENGTH,
		WM_PAINT,
		WM_CLOSE,
		WM_QUERYENDSESSION,
		WM_QUERYOPEN = 19u,
		WM_ENDSESSION = 22u,
		WM_QUIT = 18u,
		WM_ERASEBKGND = 20u,
		WM_SYSCOLORCHANGE,
		WM_SHOWWINDOW = 24u,
		WM_WININICHANGE = 26u,
		WM_SETTINGCHANGE = 26u,
		WM_DEVMODECHANGE,
		WM_ACTIVATEAPP,
		WM_FONTCHANGE,
		WM_TIMECHANGE,
		WM_CANCELMODE,
		WM_SETCURSOR,
		WM_MOUSEACTIVATE,
		WM_CHILDACTIVATE,
		WM_QUEUESYNC,
		WM_GETMINMAXINFO,
		WM_PAINTICON = 38u,
		WM_ICONERASEBKGND,
		WM_NEXTDLGCTL,
		WM_SPOOLERSTATUS = 42u,
		WM_DRAWITEM,
		WM_MEASUREITEM,
		WM_DELETEITEM,
		WM_VKEYTOITEM,
		WM_CHARTOITEM,
		WM_SETFONT,
		WM_GETFONT,
		WM_SETHOTKEY,
		WM_GETHOTKEY,
		WM_QUERYDRAGICON = 55u,
		WM_COMPAREITEM = 57u,
		WM_GETOBJECT = 61u,
		WM_COMPACTING = 65u,
		WM_COMMNOTIFY = 68u,
		WM_WINDOWPOSCHANGING = 70u,
		WM_WINDOWPOSCHANGED,
		WM_POWER,
		WM_COPYDATA = 74u,
		WM_CANCELJOURNAL,
		WM_NOTIFY = 78u,
		WM_INPUTLANGCHANGEREQUEST = 80u,
		WM_INPUTLANGCHANGE,
		WM_TCARD,
		WM_HELP,
		WM_USERCHANGED,
		WM_NOTIFYFORMAT,
		WM_CONTEXTMENU = 123u,
		WM_STYLECHANGING,
		WM_STYLECHANGED,
		WM_DISPLAYCHANGE,
		WM_GETICON,
		WM_SETICON,
		WM_NCCREATE,
		WM_NCDESTROY,
		WM_NCCALCSIZE,
		WM_NCHITTEST,
		WM_NCPAINT,
		WM_NCACTIVATE,
		WM_GETDLGCODE,
		WM_SYNCPAINT,
		WM_NCMOUSEMOVE = 160u,
		WM_NCLBUTTONDOWN,
		WM_NCLBUTTONUP,
		WM_NCLBUTTONDBLCLK,
		WM_NCRBUTTONDOWN,
		WM_NCRBUTTONUP,
		WM_NCRBUTTONDBLCLK,
		WM_NCMBUTTONDOWN,
		WM_NCMBUTTONUP,
		WM_NCMBUTTONDBLCLK,
		WM_NCXBUTTONDOWN = 171u,
		WM_NCXBUTTONUP,
		WM_NCXBUTTONDBLCLK,
		WM_INPUT = 255u,
		WM_KEYFIRST,
		WM_KEYDOWN = 256u,
		WM_KEYUP,
		WM_CHAR,
		WM_DEADCHAR,
		WM_SYSKEYDOWN,
		WM_SYSKEYUP,
		WM_SYSCHAR,
		WM_SYSDEADCHAR,
		WM_UNICHAR = 265u,
		WM_KEYLAST = 264u,
		WM_IME_STARTCOMPOSITION = 269u,
		WM_IME_ENDCOMPOSITION,
		WM_IME_COMPOSITION,
		WM_IME_KEYLAST = 271u,
		WM_INITDIALOG,
		WM_COMMAND,
		WM_SYSCOMMAND,
		WM_TIMER,
		WM_HSCROLL,
		WM_VSCROLL,
		WM_INITMENU,
		WM_INITMENUPOPUP,
		WM_MENUSELECT = 287u,
		WM_MENUCHAR,
		WM_ENTERIDLE,
		WM_MENURBUTTONUP,
		WM_MENUDRAG,
		WM_MENUGETOBJECT,
		WM_UNINITMENUPOPUP,
		WM_MENUCOMMAND,
		WM_CHANGEUISTATE,
		WM_UPDATEUISTATE,
		WM_QUERYUISTATE,
		WM_CTLCOLOR = 25u,
		WM_CTLCOLORMSGBOX = 306u,
		WM_CTLCOLOREDIT,
		WM_CTLCOLORLISTBOX,
		WM_CTLCOLORBTN,
		WM_CTLCOLORDLG,
		WM_CTLCOLORSCROLLBAR,
		WM_CTLCOLORSTATIC,
		WM_MOUSEFIRST = 512u,
		WM_MOUSEMOVE = 512u,
		WM_LBUTTONDOWN,
		WM_LBUTTONUP,
		WM_LBUTTONDBLCLK,
		WM_RBUTTONDOWN,
		WM_RBUTTONUP,
		WM_RBUTTONDBLCLK,
		WM_MBUTTONDOWN,
		WM_MBUTTONUP,
		WM_MBUTTONDBLCLK,
		WM_MOUSEWHEEL,
		WM_XBUTTONDOWN,
		WM_XBUTTONUP,
		WM_XBUTTONDBLCLK,
		WM_MOUSELAST = 525u,
		WM_PARENTNOTIFY = 528u,
		WM_ENTERMENULOOP,
		WM_EXITMENULOOP,
		WM_NEXTMENU,
		WM_SIZING,
		WM_CAPTURECHANGED,
		WM_MOVING,
		WM_POWERBROADCAST = 536u,
		WM_DEVICECHANGE,
		WM_MDICREATE = 544u,
		WM_MDIDESTROY,
		WM_MDIACTIVATE,
		WM_MDIRESTORE,
		WM_MDINEXT,
		WM_MDIMAXIMIZE,
		WM_MDITILE,
		WM_MDICASCADE,
		WM_MDIICONARRANGE,
		WM_MDIGETACTIVE,
		WM_MDISETMENU = 560u,
		WM_ENTERSIZEMOVE,
		WM_EXITSIZEMOVE,
		WM_DROPFILES,
		WM_MDIREFRESHMENU,
		WM_IME_SETCONTEXT = 641u,
		WM_IME_NOTIFY,
		WM_IME_CONTROL,
		WM_IME_COMPOSITIONFULL,
		WM_IME_SELECT,
		WM_IME_CHAR,
		WM_IME_REQUEST = 648u,
		WM_IME_KEYDOWN = 656u,
		WM_IME_KEYUP,
		WM_MOUSEHOVER = 673u,
		WM_MOUSELEAVE = 675u,
		WM_NCMOUSELEAVE = 674u,
		WM_WTSSESSION_CHANGE = 689u,
		WM_TABLET_FIRST = 704u,
		WM_TABLET_LAST = 735u,
		WM_CUT = 768u,
		WM_COPY,
		WM_PASTE,
		WM_CLEAR,
		WM_UNDO,
		WM_RENDERFORMAT,
		WM_RENDERALLFORMATS,
		WM_DESTROYCLIPBOARD,
		WM_DRAWCLIPBOARD,
		WM_PAINTCLIPBOARD,
		WM_VSCROLLCLIPBOARD,
		WM_SIZECLIPBOARD,
		WM_ASKCBFORMATNAME,
		WM_CHANGECBCHAIN,
		WM_HSCROLLCLIPBOARD,
		WM_QUERYNEWPALETTE,
		WM_PALETTEISCHANGING,
		WM_PALETTECHANGED,
		WM_HOTKEY,
		WM_PRINT = 791u,
		WM_PRINTCLIENT,
		WM_APPCOMMAND,
		WM_THEMECHANGED,
		WM_HANDHELDFIRST = 856u,
		WM_HANDHELDLAST = 863u,
		WM_AFXFIRST,
		WM_AFXLAST = 895u,
		WM_PENWINFIRST,
		WM_PENWINLAST = 911u,
		WM_USER = 1024u,
		WM_REFLECT = 8192u,
		WM_APP = 32768u
	}
}