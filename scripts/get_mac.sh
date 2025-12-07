arp -n 192.168.1.1 | awk 'NR==2{print $3}'
