#!/bin/bash
# Install prerequisites for Eco Server on Linux

# While the linux build of Eco Server is unsupported, please report any 
# issues to https://github.com/StrangeLoopGames/EcoIssues/issues

if [[ $EUID -ne 0 ]]; then
  echo "This script must be run as root" 
  exit 1
fi

OS_RELEASE_ID=$(cat /etc/os-release | grep ^ID= | awk -F= '{print $2}')

case $OS_RELEASE_ID in 
"ubuntu" | "debian" | "raspbian")
  apt -y install mono-complete libcanberra-gtk-module
  ;;

"centos" | "rhel")
  yum -y install mono-complete libcanberra-gtk-module
  ;;

"fedora")
  dnf -y install mono-complete libcanberra-gtk-module
  ;;

"arch")
  pacman --noconfirm -S mono-complete libcanberra-gtk-module
  ;;

*)
  echo "Unable to determine your operating system type."
  echo "You can try to install the following packages manually"
  echo " * mono-complete"
  echo " * libcanberra-gtk-module"
  exit 1
  ;;
esac