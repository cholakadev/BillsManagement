
[![Contributors][contributors-shield]][contributors-url]
[![Stargazers][stars-shield]][stars-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]



<!-- PROJECT LOGO -->
<br />
<p align="center">
  <a href="https://github.com/othneildrew/Best-README-Template">
    <img src="images/logo.png" alt="Logo" width="80" height="80">
  </a>

  <h3 align="center">Bills Management</h3>

  <p align="center">
    <br />
    <a href="https://github.com/cholakadev/BillsManagement">View Demo</a>
    ·
    <a href="https://github.com/cholakadev/BillsManagement/issues">Report Bug</a>
    ·
    <a href="https://github.com/cholakadev/BillsManagement/issues">Request Feature</a>
  </p>
</p>



<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgements">Acknowledgements</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

<!-- [![Product Name Screen Shot][product-screenshot]](https://example.com) -->


## Built With

* [.NET 5](https://docs.microsoft.com/en-us/dotnet/core/dotnet-five)
* [Angular](https://getbootstrap.com)



<!-- GETTING STARTED -->
## Getting Started

Manual of how to set up the project locally.

### Prerequisites


<!-- * npm
  ```sh
  npm install npm@latest -g
  ``` -->

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/cholakadev/BillsManagement.git
   ```
2. Set up database
<font size="3">Test</font>
	<font size="3">2.1. Open Microsoft SQL Server Management Studio.</font>
	<font size="3">2.2. Right click on **Databases** and select **Restore Database**.				</font>
	<font size="3">2.3. Navigate to **DatabaseBackup** folder under cloned repository directory and restore it.</font>

4. Set up server
	<font size="3">3.1. Open cloned folder and run the following command:</font>
   ```sh
   cd Server\BillsManagement\BillsManagement.Core
   ```
   <font size="3">3.2.  Allow user-secrets: </font>
	  ```sh
   dotnet init
   ```
   <font size="3">3.3.  Set up the neccessery secrets: </font>
	  ```sh
   dotnet user-secrets set "Secrets:JWT_Secret" "7582ae4085c54c2c85c7b770ae720c3d"
   ```
	  ```sh
   dotnet user-secrets set "Secrets:ConnectionString" "server=.\SQLEXPRESS;database=BillsManagement;Trusted_Connection=true;MultipleActiveResultSets=true;"
   ```
	<font size="3">***Important:*** Replace server name (SQLEXPRESS) with your own SQL Server name</font>

   <font size="3">3.4. Run the server:</font>
      ```sh
   dotnet run
   ```

<!-- 4. Set up client
	3.1. Install dependencies:
   ```sh
   npm install
   ``` -->



<!-- USAGE EXAMPLES -->
## Usage


<!-- ROADMAP -->
<!-- ## Roadmap
 See the [open issues](https://github.com/othneildrew/Best-README-Template/issues) for a list of proposed features (and known issues). -->



<!-- LICENSE -->
## License

Distributed under the MIT License. 



<!-- CONTACT -->
## Contact

Facebook: [Георги Чолаков](https://facebook.com/cholakowge)

Email: cholakovge@gmail.com



<!-- ACKNOWLEDGEMENTS -->
## Acknowledgements
* [GitHub Pages](https://pages.github.com)
* [Loaders.css](https://connoratherton.com/loaders)
* [Slick Carousel](https://kenwheeler.github.io/slick)
* [Font Awesome](https://fontawesome.com)





<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/cholakadev/BillsManagement.svg?style=for-the-badge
[contributors-url]: https://github.com/cholakadev/BillsManagement/graphs/contributors
[stars-shield]: https://img.shields.io/github/stars/cholakadev/BillsManagement.svg?style=for-the-badge
[stars-url]: https://github.com/cholakadev/BillsManagement/stargazers
[license-shield]: https://img.shields.io/github/license/cholakadev/BillsManagement.svg?style=for-the-badge
[license-url]: https://github.com/cholakadev/BillsManagement/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/cholakovge
[product-screenshot]: images/screenshot.png
