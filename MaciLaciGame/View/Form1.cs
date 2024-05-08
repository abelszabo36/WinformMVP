using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;

namespace MaciLaciGame
{
    public partial class Form1 : Form
    {
        #region Properties
        private bool pauseOn = false; // a szunet es a jatek folytatasat figyelo valtozo
        private GameModel? model; // a jatek modelje
        private PictureBox hero = new(); // a hos valtozoja
        private const int cellSize = 80;// Az egyes cellák mérete
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        #region LevelDesign


        private void GenerateHero() // a hos letrehozasa es meretenek beallitasa es kezdeti elhelyezese
        {
            hero = new();
            hero.Size = new Size(cellSize, cellSize);
            hero.BackColor = Color.Black;
            Controls.Add(hero);
            hero.Location = new Point(0, menuStrip1.ClientSize.Height);
            hero.BringToFront();
        }

        private void GenerateEnemy(GameModel model) // az ellensegek letrehozasa 
        {
            int size = model.Table.TableSize;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (model.Table.GetField(i, j) == 2) // a modell matrixba ahol 2 es van ott ellenseg helyezkedik el
                    {
                        PictureBox enemy = new();
                        enemy.Size = new Size(cellSize, cellSize);
                        enemy.BackColor = Color.Red;
                        enemy.Location = new Point(j * cellSize, i * cellSize + menuStrip1.ClientSize.Height);
                        Controls.Add(enemy);
                        enemy.BringToFront();
                    }
                }
            }
        }


        private void UpdateEnemy(int x, int y) // az ellensegek poziciojanak valtoztato metodusa
        {
            PictureBox enemy = new();
            enemy.Size = new Size(cellSize, cellSize);
            enemy.BackColor = Color.Red;
            enemy.Location = new Point(x * cellSize, y * cellSize + menuStrip1.ClientSize.Height);
            Controls.Add(enemy);
            enemy.BringToFront();
        }

        private void DeleteEnemy(int x, int y) // az ellenseg regi helyenek letorolese
        {
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && control.Location.X == x * cellSize && control.Location.Y == y * cellSize + menuStrip1.ClientSize.Height && control.BackColor == Color.Red)
                {
                    Controls.Remove(control);
                }
            }
        }


        private void GenerateObstacle(GameModel model) // az akadalyok letrehozasa
        {
            int size = model.Table.TableSize;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (model.Table.GetField(i, j) == 4)
                    {
                        PictureBox obstacle = new();
                        obstacle.Size = new Size(cellSize, cellSize);
                        obstacle.BackColor = Color.Gray;
                        Controls.Add(obstacle);
                        obstacle.Location = new Point(j * cellSize, i * cellSize + menuStrip1.ClientSize.Height);
                        obstacle.BringToFront();
                    }
                }
            }
        }

        private void GenerateBaskett(GameModel model) // az osszegyujtheto kosarak letrehozasa
        {
            int size = model.Table.TableSize;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (model.Table.GetField(i, j) == 3)
                    {
                        PictureBox baskett = new();
                        baskett.Size = new Size(cellSize, cellSize);
                        baskett.BackColor = Color.Brown;
                        Controls.Add(baskett);
                        baskett.Location = new Point(j * cellSize, i * cellSize + menuStrip1.ClientSize.Height);
                        baskett.BringToFront();
                    }

                }
            }
        }

        private void GenerateMap() // a palya legeneralasa
        {
            int size = 0;
            if (model != null && model.Table != null)
            {
                size = model.Table.TableSize;
            }

            this.ClientSize = new Size(size * cellSize, size * cellSize + menuStrip1.ClientSize.Height + statusStrip1.ClientSize.Height); // jatekter meretenek beallitasa


            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {

                    PictureBox pic = new PictureBox();
                    pic.Size = new Size(cellSize, cellSize);
                    pic.BackColor = Color.Green;
                    pic.BorderStyle = BorderStyle.FixedSingle; // Szegély hozzáadása
                    pic.Margin = new Padding(1); // Cellák közötti távolság
                    Controls.Add(pic);
                    pic.Location = new Point(i * cellSize, j * cellSize + menuStrip1.ClientSize.Height);

                }
            }

        }

        #endregion

        #region NewGameAndSetUp

        private void ClearPreviousElements() // minden palyan levo Picturebox letorlese
        {

            List<Control> controlsToRemove = new List<Control>();
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox)
                {
                    controlsToRemove.Add(control);
                }
            }

            foreach (Control control in controlsToRemove)
            {
                control.Dispose();
                Controls.Remove(control);
            }

        }


        private void SetUpGame(GameModel.Levels level) // az alkalmazast beallito metodus minden valtozot kezdoertekre allit es meghivja a generalo metodusokat
        {

            ClearPreviousElements();

            pauseOn = false;
            szünetToolStripMenuItem.Text = "Szünet";
            basketLabel.Text = "0";
            timerLabel.Text = "0";
            model = new GameModel();
            model.NewGame(level);
            model.Motion += MotionHandling;
            model.Collect += CollectionHandling;
            model.Result += EndHandle;
            GenerateHero();
            GenerateEnemy(model);
            GenerateBaskett(model);
            GenerateObstacle(model);
            GenerateMap();
            gameTimer.Start();
        }

      
        #endregion

        #region Timing

        private void gameTimer_Tick(object sender, EventArgs e) // minden masodpercben lefuto metodus
        {
            if (model != null)
            {
                model.TimeTick();
                timerLabel.Text = model.Timer.ToString();
                UpdateEnemyPositions(model); // ez valtoztatja az ellenseg poziciojat
            }
        }


        private void UpdateEnemyPositions(GameModel model) // ellenseg poziciojanak a valtozasaert felel
        {
            try
            {
                if (model == null || model.Table == null)
                    throw new ArgumentNullException();

                int size = model.Table.TableSize;
                for (int i = 0; i < size; i++) 
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (model.Table.GetField(i, j) == 2) // letorli az ellenseg regi poziciojat
                        {
                            DeleteEnemy(j, i);
                        }
                    }
                }

                model.EnemyMove(); // valtoztatja a modellbe az ellenseg poziciojat

                for (int i = 0; i < size; i++) // legeneralja az ujjon letrehozot poziciokat
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (model.Table.GetField(i, j) == 2)
                        {
                            UpdateEnemy(j, i);
                        }
                    }
                }
            } catch(ArgumentNullException e)
            {
                MessageBox.Show("Hiba történt: " + e.Message);
            }
        }
        #endregion

        #region LoadPauseExit
        private void LoadGame(object sender, EventArgs e)
        {

            SetUpGame(GameModel.Levels.Easy); // kezdetben a legkisebb palyat tolti be a rendszer
        }

        private void PauseClick(object sender, EventArgs e) // ezzel allitodik be hogy a jatekos tudja megallitani az idot es utana ujra inditani
        {
            if (pauseOn == false)
            {
                gameTimer.Stop();
                szünetToolStripMenuItem.Text = "Folytatás";
                pauseOn = true;
            }
            else
            {
                gameTimer.Start();
                szünetToolStripMenuItem.Text = "Szünet";
                pauseOn = false;
            }
        }

        private void QuitClick(object sender, EventArgs e) // a kilepeshez szukseges metodus
        {
            pauseOn = true;
            gameTimer.Stop();

            DialogResult result = MessageBox.Show("Biztos ki szeretnél lépni a játéból?", "Figyelem", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                pauseOn = false;
                gameTimer.Start();
            }
        }

        #endregion

        #region SelectLevel 
        private void kicsiPályaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetUpGame(GameModel.Levels.Easy);
        }

        private void közepesPályaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetUpGame(GameModel.Levels.Medium);
        }

        private void nagyPályaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetUpGame(GameModel.Levels.Hard);
        }

        #endregion

        #region EventHandlers

        private void CollectionHandling(object? sender, CollectedEventArgs e) // a kosarak begyujteset szolgalo esemenykezelo
        {
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && control.Location.X == e.Y * cellSize && control.Location.Y == e.X * cellSize + menuStrip1.ClientSize.Height && control.BackColor == Color.Brown)
                {

                    Controls.Remove(control);
                }
            }
            basketLabel.Text = model?.Collected.ToString();
        }

        private void MotionHandling(object? sender, MotionEventArgs e) // a mozgast kezelo esemenykezelo
        {
            hero.Location = new Point(e.X, e.Y);
        }


        private void EndHandle(object? sender, ResultEventArgs e) // a jatek veget kezelo esemenykezelo
        {
            if (e.Win)
            {
                gameTimer.Stop();
                pauseOn = true;
                MessageBox.Show("Gratulálok nyertél. Az elért idõd " + model?.Timer.ToString() + " másodperc", "Nyertél", MessageBoxButtons.OK);


            }
            else if (e.Lose)
            {
                gameTimer.Stop();
                pauseOn = true;
                MessageBox.Show("Sajnálom vesztettél", "Vesztettél", MessageBoxButtons.OK);

            }
        }

        private void KeyPressed(object sender, KeyEventArgs e) // a mozgast vegzo esemenykezelo
        {
            int currentX = hero.Location.X;
            int currentY = hero.Location.Y;

            if (pauseOn == false)
            {
                if (e.KeyCode == Keys.A)
                {
                    if ((currentX / cellSize) > 0)
                    {

                        model?.HeroMove(currentX, currentY, cellSize, e.KeyCode);
                        hero.BringToFront();
                    }
                }
                else if (e.KeyCode == Keys.D)
                {
                    if ((currentX / cellSize) < model?.Table.TableSize - 1)
                    {

                        model?.HeroMove(currentX, currentY, cellSize, e.KeyCode);
                        hero.BringToFront();
                    }
                }
                else if (e.KeyCode == Keys.S)
                {
                    if ((currentY / cellSize) < model?.Table.TableSize - 1)
                    {
                        model?.HeroMove(currentX, currentY, cellSize, e.KeyCode);
                        hero.BringToFront();
                    }
                }
                else if (e.KeyCode == Keys.W)
                {
                    if ((currentY / cellSize) > 0)
                    {

                        model?.HeroMove(currentX, currentY, cellSize, e.KeyCode);
                        hero.BringToFront();
                    }
                }

            }
        }


    }

    #endregion

}