﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_Ejercicio03_FichaDePersonajes
{
    public partial class Form1 : Form {
        private String[] personajesMagicos = { "Mago", "Nigromante" };
        String[] personajesMundanos = { "Arquero", "Daguero", "Cazador", "Guerrero", "Paladin" };
        int[] valoresAtributosAleatorios = new int[10]; private bool dadoApagado = false;
        private int numTirada = 0, ptosRepAtrib = Constantes.PTOS_REPARTIR_ATB, habPorSelect = Constantes.HABILIDADES_SELECCIONABLES;
        private Random rnd = new Random(); 

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            obtenerValoresAleatorios(); // Relleno el arrays de valores de los atributos con números aleatorios.
            lblPuntosRepartirA.Text = Constantes.PTOS_A_REP + ptosRepAtrib;
            lblHabilidadesPorSelec.Text = Constantes.HAB_POR_SELEC + habPorSelect;
            // A todos los picture box que están en el panel de atributos les cambio su .Tag a valor 0, para despúes poder 
            // jugar con los valores que se le vayan dando e incrementar o decrementar las progressbar de habilidades en 
            // función de los puntos que reparta el usuario. 
            foreach (object pbAtributo in panelAtributos.Controls) {
                if (pbAtributo is PictureBox)
                    ((PictureBox)pbAtributo).Tag = 0;
            }
        }

        private void clicCerrar(object sender, EventArgs e) {
            this.Close();
        }
        // Este método es lanzado cuando cambia el comboboxRaza. 
        private void comboboxCambiado(object sender, EventArgs e) {
            combClase.Items.Clear();
            // Los valores del combClase variarán en función de la raza seleccionada. Si el indice es 0, se cargarán 
            // los personajesMagicos en el combClase, en caso contrario se cargarán los personajesMundanos.
            if (combRaza.SelectedIndex == 0)
                combClase.Items.AddRange(personajesMagicos);
            else
                combClase.Items.AddRange(personajesMundanos);
            // Cuando cambio la raza, la clase se desselecciona, y por tanto debo resetear los valores de los atributos
            // de la clase que se hubiera seleccionado anteriormente y quitar la imagen del psj. 
            if (!combRaza.Text.Equals("")) {
                resetValoresAtrib();
                this.BackgroundImage = null;
            }
        }
        
        private void actualizarImg(object sender, EventArgs e) {
            mostrarImgPersonaje(sender);
        }

        private void mostrarImgPersonaje(object sender) {
            String personaje;
            if (!combClase.Text.Equals("")) {
                personaje = combClase.SelectedItem.ToString(); 
                // Sólo doy el plus a los atributos si quien lanzó el evento fue el cambio de clase del personaje.
                // El plus de los atributos variará en función del personaje seleccionado. 
                if (sender.Equals(combClase))
                    asignarAtributosPlusPSJ(personaje);
                // A la cadena del personaje seleccionado le quito el último carácter y lo paso a minúsculas.
                personaje = personaje.Substring(0, personaje.Length - 1);
                personaje = personaje.ToLower();

                // Si el personaje seleccionado es diferente al nigromante, que es el único personaje que 
                // no tiene género, a la cadena del personaje seleccionado le agrego una "a" en caso de que
                // el genero sea femenino y una "o" en caso de que sea masculino. 
                if (!personaje.Equals("nigromant")) {
                    if (rbtnFemenino.Checked)
                        personaje += "a";
                    if (rbtnMasculino.Checked)
                        personaje += "o";
                }
                // Evalúo la cadena personaje y establezco la imagen de fondo correspondiente al personaje seleccionado y su género. 
                switch (personaje) {
                    case "cazadoa": // Al elegir "cazador" arriba a cazador se le quita la "r" y si esta marcado el femenino se añade una "a".
                        this.BackgroundImage = Properties.Resources.cazadora;
                        break;
                    case "cazadoo": // Al elegir "cazador" arriba a cazador se le quita la "r" y si esta marcado el masculino se le añade una "o".
                        this.BackgroundImage = Properties.Resources.cazador;
                        break;
                    case "arquero":
                        this.BackgroundImage = Properties.Resources.arquero;
                        break;
                    case "arquera":
                        this.BackgroundImage = Properties.Resources.arquera;
                        break;
                    case "paladio": // Al elegir "paladin" arriba se le quita la "n" y si está marcado el masculino se añade una "o".
                        this.BackgroundImage = Properties.Resources.paladin;
                        break;
                    case "paladia": // Al elegir "paladin" arriba se le quita la "n" y si está marcado el femenino se añade una "a". 
                        this.BackgroundImage = Properties.Resources.paladina;
                        break;
                    case "daguero":
                        this.BackgroundImage = Properties.Resources.daguero;
                        break;
                    case "daguera":
                        this.BackgroundImage = Properties.Resources.daguera;
                        break;
                    case "guerrero":
                        this.BackgroundImage = Properties.Resources.guerrero;
                        break;
                    case "guerrera":
                        this.BackgroundImage = Properties.Resources.guerrera;
                        break;
                    case "mago":
                        this.BackgroundImage = Properties.Resources.mago;
                        break;
                    case "maga":
                        this.BackgroundImage = Properties.Resources.maga;
                        break;
                    case "nigromant":
                        this.BackgroundImage = Properties.Resources.nigromante;
                        break;
                }
            }
        }
        // Doy un valor extra a cada atributo en función del personaje seleccionado. 
        private void asignarAtributosPlusPSJ(String personaje) {
            switch (personaje) {
                case "Mago":
                    pbCarisma.Value = Constantes.MAGO_CARISMA_PLUS;
                    pbCoraje.Value = Constantes.MAGO_CORAJE_PLUS;
                    pbDestreza.Value = Constantes.MAGO_DESTREZA_PLUS;
                    pbFuerza.Value = Constantes.MAGO_FUERZA_PLUS;
                    pbIngenio.Value = Constantes.MAGO_INGENIO_PLUS;
                    pbIniciativa.Value = Constantes.MAGO_INICIATIVA_PLUS;
                    pbPercepcion.Value = Constantes.MAGO_PERCEPCION_PLUS;
                    pbReflejos.Value = Constantes.MAGO_REFLEJOS_PLUS;
                    pbVelocidad.Value = Constantes.MAGO_VELOCIDAD_PLUS;
                    pbVitalidad.Value = Constantes.MAGO_VITALIDAD_PLUS;
                    break;
                case "Guerrero":
                    pbCarisma.Value = Constantes.GUERRERO_CARISMA_PLUS;
                    pbCoraje.Value = Constantes.GUERRERO_CORAJE_PLUS;
                    pbDestreza.Value = Constantes.GUERRERO_DESTREZA_PLUS;
                    pbFuerza.Value = Constantes.GUERRERO_FUERZA_PLUS;
                    pbIngenio.Value = Constantes.GUERRERO_INGENIO_PLUS;
                    pbIniciativa.Value = Constantes.GUERRERO_INICIATIVA_PLUS;
                    pbPercepcion.Value = Constantes.GUERRERO_PERCEPCION_PLUS;
                    pbReflejos.Value = Constantes.GUERRERO_REFLEJOS_PLUS;
                    pbVelocidad.Value = Constantes.GUERRERO_VELOCIDAD_PLUS;
                    pbVitalidad.Value = Constantes.GUERRERO_VITALIDAD_PLUS;
                    break;
                case "Paladin":
                    pbCarisma.Value = Constantes.PALADIN_CARISMA_PLUS;
                    pbCoraje.Value = Constantes.PALADIN_CORAJE_PLUS;
                    pbDestreza.Value = Constantes.PALADIN_DESTREZA_PLUS;
                    pbFuerza.Value = Constantes.PALADIN_FUERZA_PLUS;
                    pbIngenio.Value = Constantes.PALADIN_INGENIO_PLUS;
                    pbIniciativa.Value = Constantes.PALADIN_INICIATIVA_PLUS;
                    pbPercepcion.Value = Constantes.PALADIN_PERCEPCION_PLUS;
                    pbReflejos.Value = Constantes.PALADIN_REFLEJOS_PLUS;
                    pbVelocidad.Value = Constantes.PALADIN_VELOCIDAD_PLUS;
                    pbVitalidad.Value = Constantes.PALADIN_VITALIDAD_PLUS;
                    break;
                case "Nigromante":
                    pbCarisma.Value = Constantes.NIGROMANTE_CARISMA_PLUS;
                    pbCoraje.Value = Constantes.NIGROMANTE_CORAJE_PLUS;
                    pbDestreza.Value = Constantes.NIGROMANTE_DESTREZA_PLUS;
                    pbFuerza.Value = Constantes.NIGROMANTE_FUERZA_PLUS;
                    pbIngenio.Value = Constantes.NIGROMANTE_INGENIO_PLUS;
                    pbIniciativa.Value = Constantes.NIGROMANTE_INICIATIVA_PLUS;
                    pbPercepcion.Value = Constantes.NIGROMANTE_PERCEPCION_PLUS;
                    pbReflejos.Value = Constantes.NIGROMANTE_REFLEJOS_PLUS;
                    pbVelocidad.Value = Constantes.NIGROMANTE_VELOCIDAD_PLUS;
                    pbVitalidad.Value = Constantes.NIGROMANTE_VITALIDAD_PLUS;
                    break;
                case "Daguero":
                    pbCarisma.Value = Constantes.DAGUERO_CARISMA_PLUS;
                    pbCoraje.Value = Constantes.DAGUERO_CORAJE_PLUS;
                    pbDestreza.Value = Constantes.DAGUERO_DESTREZA_PLUS;
                    pbFuerza.Value = Constantes.DAGUERO_FUERZA_PLUS;
                    pbIngenio.Value = Constantes.DAGUERO_INGENIO_PLUS;
                    pbIniciativa.Value = Constantes.DAGUERO_INICIATIVA_PLUS;
                    pbPercepcion.Value = Constantes.DAGUERO_PERCEPCION_PLUS;
                    pbReflejos.Value = Constantes.DAGUERO_REFLEJOS_PLUS;
                    pbVelocidad.Value = Constantes.DAGUERO_VELOCIDAD_PLUS;
                    pbVitalidad.Value = Constantes.DAGUERO_VITALIDAD_PLUS;
                    break;
                case "Arquero":
                    pbCarisma.Value = Constantes.ARQUERO_CARISMA_PLUS;
                    pbCoraje.Value = Constantes.ARQUERO_CORAJE_PLUS;
                    pbDestreza.Value = Constantes.ARQUERO_DESTREZA_PLUS;
                    pbFuerza.Value = Constantes.ARQUERO_FUERZA_PLUS;
                    pbIngenio.Value = Constantes.ARQUERO_INGENIO_PLUS;
                    pbIniciativa.Value = Constantes.ARQUERO_INICIATIVA_PLUS;
                    pbPercepcion.Value = Constantes.ARQUERO_PERCEPCION_PLUS;
                    pbReflejos.Value = Constantes.ARQUERO_REFLEJOS_PLUS;
                    pbVelocidad.Value = Constantes.ARQUERO_VELOCIDAD_PLUS;
                    pbVitalidad.Value = Constantes.ARQUERO_VITALIDAD_PLUS;
                    break;
                case "Cazador":
                    pbCarisma.Value = Constantes.CAZADOR_CARISMA_PLUS;
                    pbCoraje.Value = Constantes.CAZADOR_CORAJE_PLUS;
                    pbDestreza.Value = Constantes.CAZADOR_DESTREZA_PLUS;
                    pbFuerza.Value = Constantes.CAZADOR_FUERZA_PLUS;
                    pbIngenio.Value = Constantes.CAZADOR_INGENIO_PLUS;
                    pbIniciativa.Value = Constantes.CAZADOR_INICIATIVA_PLUS;
                    pbPercepcion.Value = Constantes.CAZADOR_PERCEPCION_PLUS;
                    pbReflejos.Value = Constantes.CAZADOR_REFLEJOS_PLUS;
                    pbVelocidad.Value = Constantes.CAZADOR_VELOCIDAD_PLUS;
                    pbVitalidad.Value = Constantes.CAZADOR_VITALIDAD_PLUS;
                    break;
            }
            darValorAtributosAleatorios(); // Cada vez que se cambia de personaje debo volver a sumar los valores aleatorios a la base de ptos por personaje.
        }

        private void darValorAtributosAleatorios() {
            pbCarisma.Value += valoresAtributosAleatorios[0];
            pbCoraje.Value += valoresAtributosAleatorios[1];
            pbDestreza.Value += valoresAtributosAleatorios[2];
            pbFuerza.Value += valoresAtributosAleatorios[3];
            pbIngenio.Value += valoresAtributosAleatorios[4];
            pbIniciativa.Value += valoresAtributosAleatorios[5];
            pbPercepcion.Value += valoresAtributosAleatorios[6];
            pbReflejos.Value += valoresAtributosAleatorios[7];
            pbVelocidad.Value += valoresAtributosAleatorios[8];
            pbVitalidad.Value += valoresAtributosAleatorios[9];
            // Reestablezco los puntos a repartir, puesto que si he llegado aquí es porque se ha cambiado de personaje y
            // en ese caso, si se hizo un reparto de puntos para el personaje anterior fue suprimido.
            ptosRepAtrib = Constantes.PTOS_REPARTIR_ATB;
            lblPuntosRepartirA.Text = Constantes.PTOS_A_REP + ptosRepAtrib;
        }

        private void resetValoresAtrib() {
            pbCarisma.Value = 0;
            pbCoraje.Value = 0;
            pbDestreza.Value = 0;
            pbFuerza.Value = 0;
            pbIngenio.Value = 0;
            pbIniciativa.Value = 0;
            pbPercepcion.Value = 0;
            pbReflejos.Value = 0;
            pbVelocidad.Value = 0;
            pbVitalidad.Value = 0;
        }

        private void repartirPtosAtb(object sender, EventArgs e) {
            // Si hay un personaje seleccionado y el evento lo lanzó una flecha (imagen en pictureBox) de incremento, incrementaré el atributo
            // en caso de que el evento fuera lanzado al hacer clic en una flecha de decremento, decremento el valor del atributo asociado a dicha flecha. 
            if (!combClase.Text.Equals(""))
                if (ptosRepAtrib > 0 && ((PictureBox)sender).Name.StartsWith("i"))
                    incrementarAtributo(sender);
                else if (((int)((PictureBox)sender).Tag) > 0)
                    decrementarAtributo(sender);
        }

        private void incrementarAtributo(object sender) {
            // Evalúo qué flecha de incremento fue clickeada e incremento en 1 el valor de la progressBar asociada a 
            // dicha flecha. A su vez, al .Tag asociado a la flecha de decremento le doy un +1. Lo que pretendo con esto, 
            // es que únicamente se pueda decrementar cuando previamente se haya incrementado, para nunca alterar la base 
            // que se estableció al sumar los puntos asociados a cada personaje con los valores aleatorios. 
            if (sender.Equals(incVit) && pbVitalidad.Value < pbVitalidad.Maximum) {
                pbVitalidad.Value++;
                ptosRepAtrib--;
                decVit.Tag = ((int)decVit.Tag) + 1;
            } else if (sender.Equals(incCar) && pbCarisma.Value < pbCarisma.Maximum) {
                pbCarisma.Value++;
                ptosRepAtrib--;
                decCar.Tag = ((int)decCar.Tag) + 1;
            } else if (sender.Equals(incCor) && pbCoraje.Value < pbCoraje.Maximum) {
                pbCoraje.Value++;
                ptosRepAtrib--;
                decCor.Tag = ((int)decCor.Tag) + 1;
            } else if (sender.Equals(incDest) && pbDestreza.Value < pbDestreza.Maximum) {
                pbDestreza.Value++;
                ptosRepAtrib--;
                decDest.Tag = ((int)decDest.Tag) + 1;
            } else if (sender.Equals(incFuer) && pbFuerza.Value < pbFuerza.Maximum) {
                pbFuerza.Value++;
                ptosRepAtrib--;
                decFuer.Tag = ((int)decFuer.Tag) + 1;
            } else if (sender.Equals(incIng) && pbIngenio.Value < pbIngenio.Maximum) {
                pbIngenio.Value++;
                ptosRepAtrib--;
                decIng.Tag = ((int)decIng.Tag) + 1;
            } else if (sender.Equals(incIni) && pbIniciativa.Value < pbIniciativa.Maximum) {
                pbIniciativa.Value++;
                ptosRepAtrib--;
                decIni.Tag = ((int)decIni.Tag) + 1;
            } else if (sender.Equals(incPerc) && pbPercepcion.Value < pbPercepcion.Maximum) {
                pbPercepcion.Value++;
                ptosRepAtrib--;
                decPerc.Tag = ((int)decPerc.Tag) + 1;
            } else if (sender.Equals(incRef) && pbReflejos.Value < pbReflejos.Maximum) {
                pbReflejos.Value++;
                ptosRepAtrib--;
                decRef.Tag = ((int)decRef.Tag) + 1;
            } else if (sender.Equals(incVel) && pbVelocidad.Value < pbVelocidad.Maximum) {
                pbVelocidad.Value++;
                ptosRepAtrib--;
                decVel.Tag = ((int)decVel.Tag) + 1;
            } 
            lblPuntosRepartirA.Text = Constantes.PTOS_A_REP + ptosRepAtrib; // Actualizo los puntos a repartir. 
        }

        private void decrementarAtributo(object sender) {
            // Evalúo que flecha de decremento fue seleccionada y decremento el valor de la progressbar asociada a dicha flecha.
            // Sólo podre decrementar el valor de la progressbar si el tag de la flecha de decremento fue previamente incrementado (esta 
            // comprobación se hace previamente, en el evento "repartirPtosAtb" que es el que se lanza al hacer clic en la flecha de decremento).
            if (sender.Equals(decVit) && pbVitalidad.Value > 0) {
                pbVitalidad.Value--;
                ptosRepAtrib++;
                decVit.Tag = ((int)decVit.Tag) - 1;
            } else if (sender.Equals(decCar) && pbCarisma.Value > 0) {
                pbCarisma.Value--;
                ptosRepAtrib++;
                decCar.Tag = ((int)decCar.Tag) - 1;
            } else if (sender.Equals(decCor) && pbCoraje.Value > 0) {
                pbCoraje.Value--;
                ptosRepAtrib++;
                decCor.Tag = ((int)decCor.Tag) - 1;
            } else if (sender.Equals(decDest) && pbDestreza.Value > 0) {
                pbDestreza.Value--;
                ptosRepAtrib++;
                decDest.Tag = ((int)decDest.Tag) - 1;
            } else if (sender.Equals(decFuer) && pbFuerza.Value > 0) {
                pbFuerza.Value--;
                ptosRepAtrib++;
                decFuer.Tag = ((int)decFuer.Tag) - 1;
            } else if (sender.Equals(decIng) && pbIngenio.Value > 0) {
                pbIngenio.Value--;
                ptosRepAtrib++;
                decIng.Tag = ((int)decIng.Tag) - 1;
            } else if (sender.Equals(decIni) && pbIniciativa.Value > 0) {
                pbIniciativa.Value--;
                ptosRepAtrib++;
                decIni.Tag = ((int)decIni.Tag) - 1;
            } else if (sender.Equals(decPerc) && pbPercepcion.Value > 0) {
                pbPercepcion.Value--;
                ptosRepAtrib++;
                decPerc.Tag = ((int)decPerc.Tag) - 1;
            } else if (sender.Equals(decRef) && pbReflejos.Value > 0) {
                pbReflejos.Value--;
                ptosRepAtrib++;
                decRef.Tag = ((int)decRef.Tag) - 1;
            } else if (sender.Equals(decVel) && pbVelocidad.Value > 0) {
                pbVelocidad.Value--;
                ptosRepAtrib++;
                decVel.Tag = ((int)decVel.Tag) - 1;
            }
            lblPuntosRepartirA.Text = Constantes.PTOS_A_REP + ptosRepAtrib; // Actualizo los puntos a repartir. 
        }

        private void habilidadesCheckedChange(object sender, EventArgs e) {
            CheckBox cb = (CheckBox)sender; 
            if (cb.Checked) 
                habPorSelect--; // Si se ha seleccionado un checkbox decremento la cantidad de habilidades por seleccionar. 
            else
                habPorSelect++; // Si se ha seleccionado un checkbox incremento la cantidad de habilidades por seleccionar.

            if (habPorSelect == 0) {
                // Si ya se han seleccionado el máximo número de habilidades, deshabilito todas las que no estén marcadas. 
                foreach (object cb2 in panelHabilidades.Controls) {
                    if (cb2 is CheckBox)
                        if (!((CheckBox)cb2).Checked)
                            ((CheckBox)cb2).Enabled = false;
                }
            } else {
                // Si quedan habilidades por seleccionar, habilito todas las que estén desmarcadas. 
                foreach (object cb2 in panelHabilidades.Controls) {
                    if (cb2 is CheckBox)
                        if (!((CheckBox)cb2).Checked)
                            ((CheckBox)cb2).Enabled = true;
                }
            }
            lblHabilidadesPorSelec.Text = Constantes.HAB_POR_SELEC + habPorSelect;
        }

        private void tirarDado(object sender, EventArgs e) {
            String personaje;
            // Si hay un personaje seleccionado y no se ha superado el numero de tiradas permitido: 
            if (!combClase.Text.Equals("") && numTirada < Constantes.MAX_TIRADAS) {
                resetValoresAtrib(); // Pongo los atributos a 0. 
                personaje = combClase.SelectedItem.ToString(); // cojo el personaje seleccionado.
                obtenerValoresAleatorios(); // Relleno el array con valores aleatorios nuevamente. 
                asignarAtributosPlusPSJ(personaje); // Doy valor a los atributos en base al personaje, éste metodo añadirá la base aleatoria. 
                numTirada++; // Incremento número de tirada. 
            }
            // Si el dado estaba habilidato y el número de tiradas llega al máximo permitido, apago el dado y cambio la imagen del dado 
            // para que aparezca deshabilitado. 
            if (!dadoApagado && numTirada == Constantes.MAX_TIRADAS) {
                imgDado.BackgroundImage = Properties.Resources.dadoApagado;
                dadoApagado = true;
                imgDado.Enabled = false;
            }
        }

        private void obtenerValoresAleatorios() {
            // Relleno el array de valores aleatorios para los atributos. 
            for (int i = 0; i < valoresAtributosAleatorios.Length; i++)
                valoresAtributosAleatorios[i] = rnd.Next(Constantes.MIN_VALOR_ALEATORIO, Constantes.MAX_VALOR_ALEATORIO);
        }
    }
}
