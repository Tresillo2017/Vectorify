import tkinter as tk
import matplotlib.pyplot as plt
import matplotlib.animation as animation
import numpy as np
from tkinter import ttk


class Vectorify:
    def __init__(self):
        self.create_ui()

    def create_ui(self):
        self.window = tk.Tk()
        self.window.title("Vectorify")
        self.window.geometry("700x700")
        self.window.resizable(False, False)
        self.window.config(bg="#f2f2f2")

        self.create_menu()

        title_label = tk.Label(self.window, text="Introduce las coordenadas del vector:", font=(
            "Roboto", 20), bg="#f2f2f2")
        title_label.pack(pady=10, padx=20, anchor="w")

        input_frame = tk.Frame(self.window, bg="#f2f2f2")

        x_label = tk.Label(input_frame, text="x: ",
                           font=("Roboto", 12), bg="#f2f2f2")
        x_label.pack(side=tk.LEFT, padx=5)

        self.x_entry = tk.Entry(input_frame, width=10, font=("Roboto", 12))
        self.x_entry.pack(side=tk.LEFT, padx=5)
        self.x_entry.config(validate="key", validatecommand=(
            self.x_entry.register(self.validate_input), "%P"))

        y_label = tk.Label(input_frame, text="y: ",
                           font=("Roboto", 12), bg="#f2f2f2")
        y_label.pack(side=tk.LEFT, padx=5)

        self.y_entry = tk.Entry(input_frame, width=10, font=("Roboto", 12))
        self.y_entry.pack(side=tk.LEFT, padx=5)
        self.y_entry.config(validate="key", validatecommand=(
            self.y_entry.register(self.validate_input), "%P"))

        input_frame.pack()
        input_frame1 = tk.Frame(self.window, bg="#f2f2f2")

        self.error_label = tk.Label(
            self.window, text="", fg="red", font=("Roboto", 12), bg="#f2f2f2")
        self.error_label.pack(pady=5)  # error

        title_label = tk.Label(self.window, text="Introduce un punto:", font=(
            "Roboto", 20), bg="#f2f2f2")
        title_label.pack(pady=10, padx=20, anchor="w")

        x = tk.Label(input_frame1, text="x: ",
                     font=("Roboto", 12), bg="#f2f2f2")
        x.pack(side=tk.LEFT, padx=5)

        self.x = tk.Entry(input_frame1, width=10, font=("Roboto", 12))
        self.x.pack(side=tk.LEFT, padx=5)
        self.x.config(validate="key", validatecommand=(
            self.x_entry.register(self.validate_input), "%P"))

        y = tk.Label(input_frame1, text="y: ",
                     font=("Roboto", 12), bg="#f2f2f2")
        y.pack(side=tk.LEFT, padx=5)

        self.y = tk.Entry(input_frame1, width=10, font=("Roboto", 12))
        self.y.pack(side=tk.LEFT, padx=5)
        self.y.config(validate="key", validatecommand=(
            self.y_entry.register(self.validate_input), "%P"))

        input_frame1.pack()

        calculate_button = tk.Button(self.window, text="Calcular", command=self.calculate_vector, font=(
            "Roboto", 12), bg="#4285f4", fg="white", activebackground="#3367d6", activeforeground="white")
        calculate_button.pack(pady=10)

        self.result_label = tk.Label(
            self.window, text="", font=("Roboto", 12), bg="#f2f2f2")
        self.result_label.pack(pady=5)

        self.equations_frame = tk.Frame(self.window, bg="#f2f2f2")
        self.equations_frame.pack()

        author_label = tk.Label(
            self.window, text="Hecho por Tomás Palma", font=("Roboto", 10), bg="#f2f2f2")
        author_label.pack(side=tk.BOTTOM, pady=10)

        self.animate_opening()

        # Cambios para que la interfaz se parezca a la de Google
        self.window.style = ttk.Style()
        self.window.style.theme_use("clam")
        self.window.style.configure(
            "TLabel", background="#f2f2f2", foreground="#1a73e8", font=("Roboto", 14))
        self.window.style.configure(
            "TEntry", background="#fff", foreground="#1a73e8", font=("Roboto", 12))
        self.window.style.configure("TButton", background="#1a73e8", foreground="#fff", font=(
            "Roboto", 12), activebackground="#0d47a1", activeforeground="#fff")
        self.window.style.configure("TFrame", background="#f2f2f2")

        self.window.mainloop()

    def create_menu(self):
        menu_bar = tk.Menu(self.window)
        self.window.config(menu=menu_bar)

        background_menu = tk.Menu(menu_bar, tearoff=0)
        background_menu.add_command(
            label="Rectas", command=self.calculate_vector)
        background_menu.add_command(
            label="Calculo Vectorial", command=self.calculo_vectorial)
        menu_bar.add_cascade(label="Modo", menu=background_menu)

    def validate_input(self, value):
        if value.isdigit() or value == "":
            return True
        else:
            self.error_label.config(
                text="Ingresa valores numéricos para las coordenadas del vector")
            return False

    def calculate_vector(self):
        self.error_label.config(text="")
        self.result_label.config(text="")
        if self.x_entry.get() == "" or self.y_entry.get() == "":
            self.error_label.config(
                text="Ingresa valores numéricos para las coordenadas del vector")
            return
        vx = float(self.x_entry.get())
        vy = float(self.y_entry.get())
        
        xp = float(self.x.get())
        yp = float(self.y.get())
        vector = np.array([vx, vy])
        punto = np.array([xp, yp ])
        self.draw_vector(punto, vector)
        self.show_equations(vector)

    def draw_vector(self, punto, vector):
        fig, ax = plt.subplots()
        ax.set_xlim([-10, 10])
        ax.set_ylim([-10, 10])
        x_ini=vector[0]/vector[1]*(-1000-punto[1])+punto[0]
        x_fin=vector[0]/vector[1]*(1000-punto[1])+punto[0]
        # ax.plot([ax.get_xlim()[0], ax.get_ylim()[0]], [ax.get_xlim()[1], ax.get_ylim()[1]], linestyle='--', color='red', linewidth=4, label='Recta')  # colorear la recta
        ax.plot([x_ini, x_fin], [-1000, 1000], linestyle='--', color='red', linewidth=4, label='Recta')  # colorear la recta
        ax.plot(punto[0], punto[1], 'o', color='green', linewidth=5, label='Punto')
        # print(ax.get_xlim())
        ax.quiver(0, 0, vector[0], vector[1], angles='xy', scale_units='xy', scale=1)
        ax.set_aspect('equal')
        ax.grid()
        # ani = animation.FuncAnimation(
        #     fig, self.animate_vector, frames=10, fargs=(vector,), repeat=True)
        ax.legend()
        plt.show()

    # def animate_vector(self, i, vector):
    #     plt.gca().lines[0].set_data([0, vector[0]*i/10], [0, vector[1]*i/10])
    #     return plt.gca().lines
    
    def calculo_vectorial()
    # crear ui 
    # preguntar por los vectores
    # caclular la suma y print

    def calcular_recta(punto, vector):
        return {vector[0]}/{vector[1]} * ()
        
    def show_equations(self, vector):
        self.equations_frame.destroy()
        self.equations_frame = tk.Frame(self.window, bg="#f2f2f2")
        self.equations_frame.pack()
        ecuacion_general = f"{vector[0]}x + {vector[1]}y = 0"
        ecuacion_parametrica = f"x = {vector[0]}t, y = {vector[1]}t"
        ecuacion_continua = f"y = {vector[1]}/{vector[0]}x"
        ecuacion_punto_pendiente = f"y - 0 = {vector[1]}/{vector[0]}(x - 0)"

        ecuacion_general_label = tk.Label(
            self.equations_frame, text=f"Ecuación general: {ecuacion_general}", font=("Roboto", 12), bg="#f2f2f2")
        ecuacion_general_label.pack(anchor="w")

        ecuacion_parametrica_label = tk.Label(
            self.equations_frame, text=f"Ecuación paramétrica: {ecuacion_parametrica}", font=("Roboto", 12), bg="#f2f2f2")
        ecuacion_parametrica_label.pack(anchor="w")

        ecuacion_continua_label = tk.Label(
            self.equations_frame, text=f"Ecuación continua: {ecuacion_continua}", font=("Roboto", 12), bg="#f2f2f2")
        ecuacion_continua_label.pack(anchor="w")

        ecuacion_punto_pendiente_label = tk.Label(
            self.equations_frame, text=f"Ecuación punto-pendiente: {ecuacion_punto_pendiente}", font=("Roboto", 12), bg="#f2f2f2")
        ecuacion_punto_pendiente_label.pack(anchor="w")

    def animate_opening(self):
        self.window.attributes("-alpha", 0.0)
        self.window.deiconify()
        self.window.update()
        self.window.geometry("400x400+500+300")
        self.window.attributes("-alpha", 1.0)
        self.window.update()
        self.window.geometry("400x400+400+300")
        self.window.update()
        self.window.geometry("400x400+500+300")
        self.window.update()
        self.window.geometry("500x500+400+300")
        self.window.update()


if __name__ == "__main__":
    app = Vectorify()
