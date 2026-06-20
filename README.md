## Overview

**Disk Benchmark** provides a streamlined graphical interface on top of the industry-standard. It allows you to configure, run, and export disk I/O performance tests without ever touching a command prompt. Whether you are evaluating a new SSD, diagnosing a sluggish hard drive, or comparing storage devices for a report, this tool gives you accurate, repeatable results presented in clean, shareable reports.

<img width="704" height="516" alt="image" src="https://github.com/user-attachments/assets/a317dbb2-0eca-48d0-a075-6146e44ff78b" />

## Understanding the Results

### Metrics Explained

| Metric                | What It Measures                                                                                                                                              | Why It Matters                                                                                                                                    |
| --------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------- |
| **MB/s (Throughput)** | The total data transfer rate in megabytes per second. This is the most intuitive measure of disk speed—how fast data can be read from or written to the disk. | **Higher is better.** Important for tasks such as copying large files, loading games, or transferring media.                                      |
| **IOPS**              | Input/Output Operations Per Second. The number of individual read or write requests the disk can handle per second.                                           | Critical for database servers, virtual machines, and workloads involving many small, random file operations. SSDs typically excel in this metric. |
| **Latency (ms)**      | The average time, in milliseconds, required for the disk to complete a single I/O request from start to finish.                                               | **Lower is better.** High latency can make a system feel sluggish, especially during random access workloads.                                     |
| **R MB/s**            | Read throughput measured independently from write operations.                                                                                                 | Indicates how quickly the disk can retrieve data. Important for application loading, boot times, and read-heavy workloads.                        |
| **W MB/s**            | Write throughput measured independently from read operations.                                                                                                 | Indicates how quickly the disk can store data. Important for saving files, installing software, and write-heavy workloads.                        |

### What These Numbers Mean in Practice

#### Sequential Read/Write (Large Block Sizes)

Tests using large block sizes such as **128K** or **256K** measure the disk's raw streaming performance. This reflects workloads such as:

* Copying large files
* Transferring media content
* Reading or writing large datasets

Typical performance ranges:

| Storage Type | Typical Sequential Throughput |
| ------------ | ----------------------------- |
| NVMe SSD     | 3,000+ MB/s                   |
| SATA SSD     | 500–550 MB/s                  |
| HDD          | 100–200 MB/s                  |

#### Random Read/Write (4K)

Tests using **4K random access** simulate real-world workloads such as:

* Application launching
* Web browsing
* Database operations
* Operating system responsiveness

This is where SSDs dramatically outperform HDDs.

Typical 4K Random Performance:

| Storage Type | Typical IOPS        |
| ------------ | ------------------- |
| SATA SSD     | 30,000–100,000 IOPS |
| HDD          | 100–300 IOPS        |

#### Mixed I/O

Mixed I/O tests simulate workloads where reads and writes occur simultaneously, such as an operating system drive during normal use.

Examples include:

* 70% Read / 30% Write (**70R/30W**)
* 50% Read / 50% Write (**50R/50W**)
* 30% Read / 70% Write (**30R/70W**)

The read/write ratio determines the balance between retrieval and storage operations and can significantly affect overall performance.

<img width="794" height="550" alt="image" src="https://github.com/user-attachments/assets/b576fa97-847c-41a9-8b2a-2cc7a22da40d" />


