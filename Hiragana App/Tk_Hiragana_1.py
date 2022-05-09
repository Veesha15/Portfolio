import tkinter as tk
import itertools
import PIL.Image
import PIL.ImageTk
import tkinter.font
import random

# <editor-fold desc="Root">
root = tk.Tk()
root.title("Hiragana Drills")
root.iconbitmap("ArtWork/Icons/Japanese_Flag.ico")
root.resizable(False, False)
root.configure(background='white')
# </editor-fold>

# <editor-fold desc="Fonts + Colours">
main_font = tkinter.font.Font(family="Tempus Sans ITC", size=24, weight="bold")
how_to_font = tkinter.font.Font(family="Tempus Sans ITC", size=20)
sound_font = tkinter.font.Font(family="Eras Medium ITC", size=18)  # Arial Nova  # Bahnschrift
top_font = tkinter.font.Font(family="Eras Medium ITC", size=14)  # Arial Nova  # Bahnschrift

ginger = "#BE5504"
charcoal = "#333333"
cleanblue = "#96CDEE"
marigold = "#F6A50E"
greyblue = "#90ABE2"
# </editor-fold>

# <editor-fold desc="Images">
bamboo_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Images/Bamboo.png"))
koi_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Images/Koi.png"))
kabuki_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Images/Kabuki.png"))
game_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Images/Board_Game.png"))
pattern_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Images/Wave_Pattern.png"))
question_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Images/Question_Mark.png"))
arrow_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Images/Arrow.png"))

A_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/A.png"))
I_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/I.png"))
U_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/U.png"))
E_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/E.png"))
O_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/O.png"))

KA_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/KA.png"))
KI_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/KI.png"))
KU_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/KU.png"))
KE_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/KE.png"))
KO_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/KO.png"))

SA_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/SA.png"))
SHI_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/SHI.png"))
SU_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/SU.png"))
SE_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/SE.png"))
SO_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/SO.png"))

TA_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/TA.png"))
CHI_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/CHI.png"))
TSU_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/TSU.png"))
TE_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/TE.png"))
TO_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/TO.png"))

NA_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/NA.png"))
NI_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/NI.png"))
NU_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/NU.png"))
NE_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/NE.png"))
NO_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/NO.png"))

HA_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/HA.png"))
HI_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/HI.png"))
FU_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/FU.png"))
HE_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/HE.png"))
HO_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/HO.png"))

MA_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/MA.png"))
MI_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/MI.png"))
MU_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/MU.png"))
ME_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/ME.png"))
MO_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/MO.png"))

YA_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/YA.png"))
YU_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/YU.png"))
YO_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/YO.png"))

RA_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/RA.png"))
RI_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/RI.png"))
RU_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/RU.png"))
RE_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/RE.png"))
RO_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/RO.png"))

WA_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/WA.png"))
WI_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/WI.png"))
WE_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/WE.png"))
WO_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/WO.png"))

N_img = PIL.ImageTk.PhotoImage(PIL.Image.open("ArtWork/Symbols/N.png"))
# </editor-fold>

# <editor-fold desc="Text">
text_column = "- Select the column you want to practise \n"
text_random = "- Select 'Random' for five random characters \n"
text_refresh = "- Select 'Refresh' to play again"
text_display = "- The English sounds will be displayed here \n"
text_canvas = "- Draw the corresponding Hiragana character\n  on the canvas below \n"
text_reveal = "- Select a button to reveal the answer"
text_draw = "- Press and hold the left mouse button to draw\n"
text_clear = "- Right click on a canvas to clear it"
# </editor-fold>

# <editor-fold desc="Frames">
deco_window = tk.Frame(root)
deco_window.pack(side="left", padx=(10, 0), pady=(0, 10))

deco_pic = tk.Label(deco_window, image=bamboo_img, bd=1, relief='solid', highlightbackground=charcoal)
deco_pic.pack()

select_window = tk.Frame(root)
select_window.pack(fill="x", padx=10)

alphabet_window = tk.Frame(select_window)
alphabet_window.pack(side="left")

alphabet_window_N = tk.Frame(alphabet_window)
alphabet_window_N.pack(expand=True, fill="both")

alphabet_window_S = tk.Frame(alphabet_window)
alphabet_window_S.pack(expand=True, fill="both")

how_to_window = tk.Frame(select_window)
how_to_window.pack(expand=True, fill="both")

reveal_window = tk.Frame(root)
reveal_window.pack(fill="x", padx=10, pady=10)

reveal_bg = tk.Label(reveal_window, image=pattern_img)
reveal_bg.place(x=0, y=0, relwidth=1, relheight=1)

paint_window = tk.Frame(root, bg="white")
paint_window.pack(padx=5, pady=(0, 10))
# </editor-fold>

random_pos = [0, 1, 2, 3, 4]
selected_column = ""

columns_dict = {
            "a": {"sound": ["A", "I", "U", "E", "O"], "symbol": [A_img, I_img, U_img, E_img, O_img]},
            "k": {"sound": ["KA", "KI", "KU", "KE", "KO"], "symbol": [KA_img, KI_img, KU_img, KE_img, KO_img]},
            "s": {"sound": ["SA", "SHI", "SU", "SE", "SO"], "symbol": [SA_img, SHI_img, SU_img, SE_img, SO_img]},
            "t": {"sound": ["TA", "CHI", "TSU", "TE", "TO"], "symbol": [TA_img, CHI_img, TSU_img, TE_img, TO_img]},
            "n": {"sound": ["NA", "NI", "NU", "NE", "NO"], "symbol": [NA_img, NI_img, NU_img, NE_img, NO_img]},
            "h": {"sound": ["HA", "HI", "FU", "HE", "HO"], "symbol": [HA_img, HI_img, FU_img, HE_img, HO_img]},
            "m": {"sound": ["MA", "MI", "MU", "ME", "MO"], "symbol": [MA_img, MI_img, MU_img, ME_img, MO_img]},
            "y": {"sound": ["YA", "YU", "YO", "--", "--"], "symbol": [YA_img, YU_img, YO_img, question_img, question_img]},
            "r": {"sound": ["RA", "RI", "RU", "RE", "RO"], "symbol": [RA_img, RI_img, RU_img, RE_img, RO_img]},
            "w": {"sound": ["WA", "WI", "WE", "WO", "--"], "symbol": [WA_img, WI_img, WE_img, WO_img, question_img]},
            }


def withdraw_info(event):
    InfoTop.top.withdraw()


def display_info(event):
    InfoTop.top.deiconify()


class InfoTop:
    top = tk.Toplevel()
    top.title("INFO")
    top.iconbitmap("ArtWork/Icons/Question_Mark.ico")
    top.resizable(False, False)
    top_x = root.winfo_x() + 950
    top_y = root.winfo_y() + 10
    top.geometry(f"+{top_x}+{top_y}")

    def __init__(self, text):
        self.window = tk.Frame(InfoTop.top)
        self.window.pack(anchor="w")

        self.arrow = tk.Label(self.window, image=arrow_img, height=140)
        self.arrow.pack(side="left", expand=True, fill="both")

        self.info_text = tk.Label(self.window, text=text, justify="left")
        self.info_text.configure(font=top_font)
        self.info_text.pack(side="right", expand=True, fill="y")


class RefreshButton:
    def __init__(self):
        self.btn_refresh = tk.Button(alphabet_window_N, image=koi_img, text="Refresh", command=self.click_refresh,
                                bg="white", fg=cleanblue, bd=1, padx=10, compound="left", anchor="w")
        self.btn_refresh.configure(font=main_font)
        self.btn_refresh.pack(fill="both", expand=True)

    def click_refresh(self):
        random.shuffle(random_pos)

        for column_btn in column_btn_list:
            column_btn.enable_columns()

        for reveal_btn in reveal_btn_list:
            reveal_btn.default_state()
            reveal_btn.disable_reveal()

        for paint_canvas in paint_canvas_list:
            paint_canvas.canvas.delete("all")

        RandomButton.enable_random(random_btn)


class RandomButton:
    def __init__(self):
        self.btn_random = tk.Button(alphabet_window_S, image=kabuki_img, text="Random", command=self.click_random,
                               bg="white", fg=marigold, bd=1, padx=10, compound="left", anchor="w")
        self.btn_random.configure(font=main_font)
        self.btn_random.pack(fill="both", expand=True)

    def disable_random(self):
        self.btn_random.config(state="disable")

    def enable_random(self):
        self.btn_random.config(state="normal")

    def click_random(self):
        global selected_column
        selected_column = "x"

        all_columns_s1 = [columns_dict[column]["sound"] for column in columns_dict.keys()]
        all_columns_s2 = list(itertools.chain(*all_columns_s1))
        all_columns_s3 = [i for i in all_columns_s2 if i != "--"]

        all_images_s1 = [columns_dict[column]["symbol"] for column in columns_dict.keys()]
        all_images_s2 = list(itertools.chain(*all_images_s1))
        all_images_s3 = [i for i in all_images_s2 if i != question_img]

        pos = random.sample(range(46), k=5)
        random_columns = [all_columns_s3[n] for n in pos]
        random_images = [all_images_s3[n] for n in pos]

        columns_dict["x"] = {"sound": random_columns, "symbol": random_images}

        for reveal_btn in reveal_btn_list:
            reveal_btn.update_text()
            reveal_btn.enable_reveal()

        for column_btn in column_btn_list:
            column_btn.disable_columns()

        self.disable_random()


class ColumnButton:

    def __init__(self, window, column):
        self.column = column
        self.btn = tk.Button(window, text=column.upper(), width=4, height=0, bd=1, bg="white", fg=ginger,
                             command=self.display_sound)
        self.btn.pack(side="left")
        self.btn.configure(font=main_font)

    def display_sound(self):
        self.update_selected_column()

        for reveal_btn in reveal_btn_list:
            reveal_btn.update_text()
            reveal_btn.enable_reveal()

        for column_btn in column_btn_list:
            column_btn.disable_columns()

        RandomButton.disable_random(random_btn)

    def update_selected_column(self):
        global selected_column
        selected_column = self.column

    def disable_columns(self):
        self.btn.config(state="disable")

    def enable_columns(self):
        self.btn.config(state="normal")


class RevealButton:

    default_text = "..."
    default_img = question_img

    def __init__(self, padx, pos):
        self.pos = pos
        self.btn = tk.Button(reveal_window, image=self.default_img, text=self.default_text, state="disable",
                             bg="white", fg=charcoal, bd=1, compound="bottom", command=self.display_symbol)
        self.btn.configure(font=sound_font)
        self.btn.pack(side="left", expand=True, fill="both", pady=20, padx=padx)

    def display_symbol(self):
        self.update_image()

    def default_state(self):
        self.btn.config(image=self.default_img)
        self.btn.config(text=self.default_text)

    def update_text(self):
        sound_list = list(columns_dict[selected_column]["sound"])
        self.btn.config(text=sound_list[random_pos[self.pos]])

    def update_image(self):
        symbol_list = list(columns_dict[selected_column]["symbol"])
        self.btn.config(image=symbol_list[random_pos[self.pos]])

    def disable_reveal(self):
        self.btn.config(state="disable")

    def enable_reveal(self):
        self.btn.config(state="normal")


class PaintCanvas:

    def __init__(self):
        self.canvas = tk.Canvas(paint_window, bg="white", width=150, height=150,
                                highlightthickness=1, relief='ridge', highlightbackground=greyblue)
        self.canvas.pack(side="left", padx=5)
        self.canvas.bind('<B1-Motion>', self.paint)
        self.canvas.bind('<Button-3>', self.clear_canvas)

    def paint(self, event):
        x1, y1 = (event.x - 1), (event.y - 1)
        x2, y2 = (event.x + 1), (event.y + 1)
        self.canvas.create_oval(x1, y1, x2, y2, width=5)

    def clear_canvas(self, event):
        self.canvas.delete("all")


column_btn_N = [ColumnButton(alphabet_window_N, col) for col in "akstn"]
column_btn_S = [ColumnButton(alphabet_window_S, col) for col in "hmyrw"]
column_btn_list = column_btn_N + column_btn_S

refresh_btn = RefreshButton()

random_btn = RandomButton()

btn_how_to = tk.Button(how_to_window, image=game_img, text="How to play",
                       bg="white", fg=charcoal, bd=1, compound="top")
btn_how_to.configure(font=how_to_font)
btn_how_to.bind('<Leave>', withdraw_info)
btn_how_to.bind('<Enter>', display_info)
btn_how_to.pack(fill="both", expand=True)

btn_1 = RevealButton(pos=0, padx=(25, 30))
btn_2 = RevealButton(pos=1, padx=30)
btn_3 = RevealButton(pos=2, padx=30)
btn_4 = RevealButton(pos=3, padx=30)
btn_5 = RevealButton(pos=4, padx=(30, 25))
reveal_btn_list = [btn_1, btn_2, btn_3, btn_4, btn_5]

paint_canvas_list = [PaintCanvas() for x in range(5)]

info_1 = InfoTop(text_column + text_random + text_refresh)
info_2 = InfoTop(text_display + text_canvas + text_reveal)
info_3 = InfoTop(text_draw + text_clear)

InfoTop.top.withdraw()
root.mainloop()
